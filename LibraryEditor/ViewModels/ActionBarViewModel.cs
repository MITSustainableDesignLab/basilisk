﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Windows;

using Microsoft.Win32;

using Newtonsoft.Json;

using Basilisk.Controls.Attributes;
using Basilisk.Controls.InterfaceModels;
using Basilisk.Controls.Extensions;

namespace Basilisk.LibraryEditor.ViewModels
{
    public class ActionBarViewModel
    {
        private readonly MainWindowViewModel parent;

        public ActionBarViewModel(MainWindowViewModel parent)
        {
            this.parent = parent;
            CreateNewComponentCommand = new RelayCommand(CreateNewComponent, () => parent.IsAnyLibraryLoaded);
            DeleteComponentCommand = new RelayCommand(DeleteComponent, c => c != null);
            DuplicateComponentCommand = new RelayCommand(DuplicateComponent, c => c != null);
            EditSelectedItemMetadataCommand = new RelayCommand(EditComponentMetadata, o => o is LibraryComponent);
            NewLibraryCommand = new RelayCommand(NewLibrary);
            OpenLibraryCommand = new RelayCommand(path => OpenLibrary(path as string));
            SaveCommand = new RelayCommand(Save, () => parent.IsAnyLibraryLoaded);
            SaveAsCommand = new RelayCommand(SaveAs, () => parent.IsAnyLibraryLoaded);
        }

        public RelayCommand CreateNewComponentCommand { get; }
        public RelayCommand DeleteComponentCommand { get; }
        public RelayCommand DuplicateComponentCommand { get; }
        public RelayCommand EditSelectedItemMetadataCommand { get; }
        public RelayCommand NewLibraryCommand { get; }
        public RelayCommand OpenLibraryCommand { get; }
        public RelayCommand SaveCommand { get; }
        public RelayCommand SaveAsCommand { get; }

        internal bool CheckForSaveAndProceed()
        {
            var res = MessageBox.Show(
                "Save changes?",
                "Unsaved changes",
                MessageBoxButton.YesNoCancel,
                MessageBoxImage.Question);
            if (res == MessageBoxResult.Yes)
            {
                Save();
                return true;
            }
            else if (res == MessageBoxResult.No) { return true; }
            else { return false; }
        }

        private static LibraryComponent CreateComponentWithDefaults(Type type)
        {
            var newComponent = (LibraryComponent)Activator.CreateInstance(type);

            Func<Type, Dictionary<string, object>> getDefaults = t =>
            {
                if (t == null) { return new Dictionary<string, object>(); }
                return
                    t
                    .GetProperties()
                    .Where(prop => prop.GetCustomAttribute<DefaultValueAttribute>() != null)
                    .ToDictionary(prop => prop.Name, prop => prop.GetCustomAttribute<DefaultValueAttribute>().Value);
            };

            var localDefaults = getDefaults(type);
            var sourceDefaults = getDefaults(type.GetCustomAttribute<UseDefaultValuesOfAttribute>()?.SourceType);

            foreach (var prop in type.GetProperties())
            {
                object def;
                if (localDefaults.TryGetValue(prop.Name, out def) ||
                    sourceDefaults.TryGetValue(prop.Name, out def))
                {
                    prop.SetValue(newComponent, def);
                }
            }

            return newComponent;
        }

        private void CreateNewComponent(object componentType)
        {
            if (componentType == null)
            {
                throw new ArgumentNullException(nameof(componentType));
            }
            var cType = (Type)componentType;
            var newComponent = (LibraryComponent)cType.CreateComponentWithDefaults();
            var vm = new MetadataEditorViewModel(newComponent)
            {
                Category = newComponent.Category,
                EditorTitle = "New component",
                ValidateName = name =>
                    name == newComponent.Name ||
                    !parent.LoadedLibrary.WouldCollide(name, cType)

            };
            var editWindow = new ComponentMetadataEditWindow() { DataContext = vm };
            var res = editWindow.ShowDialog();
            if (res.HasValue && res.Value)
            {
                newComponent.Name = vm.Name;
                newComponent.Category = vm.Category;
                newComponent.Comments = vm.Comments;
                newComponent.DataSource = vm.DataSource;
                parent.CurrentCategorizedComponents.AddComponent(newComponent);
                newComponent.PropertyChanged += parent.SetUnsavedChangesOnPropertyChange;
                parent.HasUnsavedChanges = true;
            }
        }

        private void DeleteComponent(object component)
        {
            var c = (LibraryComponent)component;
            var referencers = parent.AllLoadedComponents.Where(cmp => cmp.DirectlyReferences(c));
            if (referencers.Any())
            {
                var denialVM = new DeletionDenialViewModel(c, referencers);
                var denier = new DeletionDenialWindow() { DataContext = denialVM };
                denier.ShowDialog();
                return;
            }
            parent.CurrentCategorizedComponents.RemoveComponent(c);
            parent.HasUnsavedChanges = true;
        }

        private void DuplicateComponent(object component)
        {
            var cType = component.GetType();
            var newComponent = ((LibraryComponent)component).Duplicate();
            while (parent.LoadedLibrary.WouldCollide(newComponent.Name, cType))
            {
                newComponent.Name += " copy";
            }
            parent.CurrentCategorizedComponents.AddComponent((dynamic)newComponent);
            newComponent.PropertyChanged += parent.SetUnsavedChangesOnPropertyChange;
            parent.HasUnsavedChanges = true;
        }

        private void EditComponentMetadata(object component)
        {
            var cType = component.GetType();
            var original = (LibraryComponent)component;
            var vm = new MetadataEditorViewModel(original)
            {
                EditorTitle = "Edit component",
                ValidateName = name =>
                    name == original.Name ||
                    !parent.LoadedLibrary.WouldCollide(name, cType)
            };
            var editWindow = new ComponentMetadataEditWindow() { DataContext = vm };
            var res = editWindow.ShowDialog();
            if (res.HasValue && res.Value)
            {
                var c = parent.SelectedComponent;
                var oldCategoryName = c.Category;
                if (original.IsNameMutable) { c.Name = vm.Name; }
                if (original.IsCategoryNameMutable) { c.Category = vm.Category; }
                c.Comments = vm.Comments;
                c.DataSource = vm.DataSource;
                if (oldCategoryName != c.Category)
                {
                    var oldCategory = parent.CurrentCategorizedComponents.Single(cat => cat.CategoryName == oldCategoryName);
                    var newCategory = parent.CurrentCategorizedComponents.SingleOrDefault(cat => cat.CategoryName == c.Category);
                    oldCategory.RemoveComponent(c);
                    if (newCategory == null)
                    {
                        parent.CurrentCategorizedComponents.AddComponent(c);
                    }
                    else
                    {
                        newCategory.AddComponent(c);
                    }
                }
                parent.CurrentCategorizedComponents.PurgeEmptyCategories();
            }
        }

        private void NewLibrary()
        {
            SetActiveLibrary(new Library());
            parent.CurrentLibraryPath = null;
            parent.HasUnsavedChanges = false;
        }

        private void OpenLibrary(string path = null)
        {
            try
            {
                if (parent.HasUnsavedChanges && !CheckForSaveAndProceed()) { return; }
                if (path == null)
                {
                    var ofd = new OpenFileDialog()
                    {
                        Title = "Select library file",
                        Filter = "Building template libraries|*.xml;*.json"
                    };
                    var res = ofd.ShowDialog();
                    if (res.HasValue && res.Value) { path = ofd.FileName; }
                }
                if (path != null)
                {
                    // We could use the extension, but there's still no way to distinguish
                    // between legacy XML libraries and new ones, so let's just try
                    // everything.
                    var newLib = default(Core.Library);
                    try
                    {
                        newLib = Core.Library.FromJson(File.ReadAllText(path));
                    }
                    catch (JsonSerializationException e)
                    {
                        MessageBox.Show(
                            $"Error loading library: {e.Message}",
                            "Error loading library",
                            MessageBoxButton.OK,
                            MessageBoxImage.Error);
                    }
                    if (newLib != null)
                    {
#if DEBUG
                        var orphanCount = newLib.OrphanedComponents().Count();
                        if (orphanCount > 0) { MessageBox.Show($"{orphanCount} orphaned object(s)"); }
#endif
                        SetActiveLibrary(Library.Create(newLib), ignoreUnsavedChanges: true);
                        parent.CurrentLibraryPath = path;
                        parent.HasUnsavedChanges = false;
                        return;
                    }
                    try
                    {
                        newLib = Core.Library.FromXml(path);
                    }
                    catch { }
                    if (newLib != null)
                    {
#if DEBUG
                        var orphanCount = newLib.OrphanedComponents().Count();
                        if (orphanCount > 0) { MessageBox.Show($"{orphanCount} orphaned object(s)"); }
#endif
                        SetActiveLibrary(Library.Create(newLib), ignoreUnsavedChanges: true);
                        parent.CurrentLibraryPath = path;
                        parent.HasUnsavedChanges = false;
                        return;
                    }
                    try
                    {
                        var legacy = Legacy.Library.Load(path);
                        newLib = Legacy.Conversion.Convert(legacy);
                    }
                    catch { }
                    if (newLib != null)
                    {
                        MessageBox.Show(
                            "This is a legacy library. Any changes you make cannot be saved back to this library file - you will have to specify a location for a new, updated file.",
                            "Legacy library loaded",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information);
#if DEBUG
                        var orphanCount = newLib.OrphanedComponents().Count();
                        if (orphanCount > 0) { MessageBox.Show($"{orphanCount} orphaned object(s)"); }
#endif
                        SetActiveLibrary(Library.Create(newLib), ignoreUnsavedChanges: true);
                        parent.CurrentLibraryPath = null;
                        parent.HasUnsavedChanges = false;
                        return;
                    }
                    throw new Exception("Unknown library format or corrupted library");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(
                    $"Error loading library: {e.Message}",
                    "Error loading library",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private void Save()
        {
            if (parent.CurrentLibraryPath == null) { SaveAs(); }
            else { SaveTo(parent.CurrentLibraryPath); }
        }

        private void SaveAs()
        {
            var sfd = new SaveFileDialog()
            {
                Title = "Select save location",
                Filter = "JSON template library|*.json|XML template library|*.xml"
            };
            var res = sfd.ShowDialog();
            if (res.HasValue && res.Value)
            {
                SaveTo(sfd.FileName);
            }
        }

        private void SaveTo(string path)
        {
            try
            {
                var coreLib = parent.LoadedLibrary.ToCoreLibrary();
#if DEBUG
                var orphanCount = coreLib.OrphanedComponents().Count();
                if (orphanCount > 0)
                {
                    MessageBox.Show($"{orphanCount} orphaned component(s)");
                }
#endif
                var extension = Path.GetExtension(path);
                if (extension == ".json")
                {
                    var json = JsonConvert.SerializeObject(parent.LoadedLibrary.ToCoreLibrary(), Formatting.Indented);
                    File.WriteAllText(path, json);
                }
                else if (extension == ".xml")
                {
                    using (var fs = new FileStream(path, FileMode.Create))
                    {
                        var serializer = new DataContractSerializer(typeof(Core.Library));
                        serializer.WriteObject(fs, parent.LoadedLibrary.ToCoreLibrary());
                    }
                }
                else
                {
                    throw new InvalidOperationException("Unknown library format");
                }
                parent.CurrentLibraryPath = path;
                parent.HasUnsavedChanges = false;
            }
            catch (Exception e)
            {
                MessageBox.Show(
                    $"Error saving library: {e.Message}",
                    "Error saving library",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private void SetActiveLibrary(Library library, bool ignoreUnsavedChanges = false)
        {
            if (parent.IsAnyLibraryLoaded)
            {
                if (parent.HasUnsavedChanges && !ignoreUnsavedChanges && !CheckForSaveAndProceed())
                {
                    return;
                }
                foreach (var component in parent.LoadedLibrary.AllModifiableComponents)
                {
                    component.PropertyChanged -= parent.SetUnsavedChangesOnPropertyChange;
                }
            }

            parent.LoadedLibrary = library;
            foreach (var component in parent.LoadedLibrary.AllModifiableComponents)
            {
                component.PropertyChanged += parent.SetUnsavedChangesOnPropertyChange;
            }
            CreateNewComponentCommand.RaiseCanExecuteChanged();
            SaveCommand.RaiseCanExecuteChanged();
            SaveAsCommand.RaiseCanExecuteChanged();
            parent.SelectedComponent = null;
        }
    }
}
