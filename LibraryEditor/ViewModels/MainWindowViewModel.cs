using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Windows;
using System.Windows.Input;
using System.Xml;
using System.Xml.Serialization;

using Microsoft.Win32;

using Newtonsoft.Json;

using Basilisk.Controls;
using Basilisk.Controls.InterfaceModels;

namespace Basilisk.LibraryEditor.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly RelayCommand createNewComponentCommand;
        private readonly RelayCommand deleteComponentCommand;
        private readonly RelayCommand duplicateComponentCommand;
        private readonly RelayCommand editSelectedItemMetadataCommand;

        private ComponentCategoryCollection currentlyCategorizedComponents;
        private string currentLibraryPath;
        private ICollection<LibraryComponent> currentlyListedComponents;
        private bool hasUnsavedChanges;
        private Library loadedLibrary;
        private LibraryComponent selectedComponent;
        private ObservableCollection<MaterialLayer> selectedComponentLayers;
        private ObservableCollection<MassRatios> selectedComponentMassRatios;

        public MainWindowViewModel()
        {
            createNewComponentCommand = new RelayCommand(CreateNewComponent, _ => IsAnyLibraryLoaded);
            deleteComponentCommand = new RelayCommand(DeleteComponent, c => c != null);
            duplicateComponentCommand = new RelayCommand(DuplicateComponent, c => c != null);
            editSelectedItemMetadataCommand = new RelayCommand(EditComponentMetadata, o => o is LibraryComponent);
            NewLibraryCommand = new RelayCommand(NewLibrary);
            OpenLibraryCommand = new RelayCommand(OpenLibrary);
            SaveCommand = new RelayCommand(Save);
            SaveAsCommand = new RelayCommand(SaveAs);
#if DEBUG
            Instance = this;
#endif
        }

#if DEBUG
        private static MainWindowViewModel Instance { get; set; }
#endif

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand CreateNewComponentCommand => createNewComponentCommand;
        public ICommand DeleteComponentCommand => deleteComponentCommand;
        public ICommand DuplicateComponentCommand => duplicateComponentCommand;
        public ICommand EditSelectedItemMetadataCommand => editSelectedItemMetadataCommand;
        public ICommand NewLibraryCommand { get; }
        public ICommand OpenLibraryCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand SaveAsCommand { get; }

        public bool IsAnyLibraryLoaded => loadedLibrary != null;

        public ComponentCategoryCollection CurrentCategorizedComponents
        {
            get { return currentlyCategorizedComponents; }
            private set
            {
                currentlyCategorizedComponents = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentCategorizedComponents)));
            }
        }

        public ICollection<LibraryComponent> CurrentComponents
        {
            get { return currentlyListedComponents; }
            set
            {
                currentlyListedComponents = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentComponents)));
                CurrentCategorizedComponents = currentlyListedComponents == null ? null : new ComponentCategoryCollection(currentlyListedComponents);
            }
        }

        public string CurrentLibraryPath
        {
            get { return currentLibraryPath; }
            set
            {
                currentLibraryPath = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(CurrentLibraryPath)));
            }
        }

        public bool HasUnsavedChanges
        {
            get { return hasUnsavedChanges; }
            set
            {
                hasUnsavedChanges = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(HasUnsavedChanges)));
            }
        }

        public IEnumerable<DaySchedule> LoadedDaySchedules => LoadedSchedules?.Select(s => s as DaySchedule).Where(s => s != null);
        public IEnumerable<WeekSchedule> LoadedWeekSchedules => LoadedSchedules?.Select(s => s as WeekSchedule).Where(s => s != null);
        public ICollection<LibraryComponent> LoadedGasMaterials => loadedLibrary?.GasMaterials;
        public ICollection<LibraryComponent> LoadedGlazingMaterials => loadedLibrary?.GlazingMaterials;
        public ICollection<LibraryComponent> LoadedOpaqueConstructions => loadedLibrary?.OpaqueConstructions;
        public ICollection<LibraryComponent> LoadedOpaqueMaterials => loadedLibrary?.OpaqueMaterials;
        public ICollection<LibraryComponent> LoadedSchedules => loadedLibrary?.Schedules;
        public ICollection<LibraryComponent> LoadedStructureDefinitions => loadedLibrary?.StructureDefinitions;
        public ICollection<LibraryComponent> LoadedWindowConstructions => loadedLibrary?.WindowConstructions;
        public ICollection<LibraryComponent> LoadedWindowMaterials => LoadedGlazingMaterials?.Concat(LoadedGasMaterials).ToArray();

        public Func<IMaterialPickable, ICollection<LibraryComponent>, bool> PickMaterial =>
            (pickable, components) =>
            {
                var categorized = new ComponentCategoryCollection(components);
                var pickerVM = new ComponentPickerViewModel()
                {
                    Components = categorized
                };
                var picker = new ComponentPicker() { DataContext = pickerVM };
                var res = picker.ShowDialog();
                if (res.HasValue && res.Value)
                {
                    pickable.Material = pickerVM.SelectedComponent;
                    return true;
                }
                return false;
            };

        public Func<YearSchedulePart, IEnumerable<LibraryComponent>, bool> PickWeekForYear =>
            (part, weeks) =>
            {
                var categorized = new ComponentCategoryCollection(weeks.ToArray());
                var pickerVM = new ComponentPickerViewModel()
                {
                    Components = categorized
                };
                var picker = new ComponentPicker() { DataContext = pickerVM };
                var res = picker.ShowDialog();
                if (res.HasValue && res.Value)
                {
                    part.Schedule = (WeekSchedule)pickerVM.SelectedComponent;
                    return true;
                }
                return false;
            };

        public LibraryComponent SelectedComponent
        {
            get { return selectedComponent; }
            set
            {
                selectedComponent = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(SelectedComponent)));
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(SelectedDayScheduleValues)));
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(SelectedWeekScheduleDays)));
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(SelectedYearScheduleParts)));
                editSelectedItemMetadataCommand.RaiseCanExecuteChanged();
                deleteComponentCommand.RaiseCanExecuteChanged();
                duplicateComponentCommand.RaiseCanExecuteChanged();
                SelectedComponentLayers = (selectedComponent as LayeredConstruction)?.Layers;
                SelectedComponentMassRatios = (selectedComponent as StructureInformation)?.MassRatios;
            }
        }

        public ObservableCollection<MaterialLayer> SelectedComponentLayers
        {
            get { return selectedComponentLayers; }
            set
            {
                selectedComponentLayers = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(SelectedComponentLayers)));
            }
        }

        public ObservableCollection<MassRatios> SelectedComponentMassRatios
        {
            get { return selectedComponentMassRatios; }
            set
            {
                selectedComponentMassRatios = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedComponentMassRatios)));
            }
        }

        public IList<double> SelectedDayScheduleValues
        {
            get { return (SelectedComponent as DaySchedule)?.Values; }
            set
            {
                ((DaySchedule)selectedComponent).Values = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(SelectedDayScheduleValues)));
            }
        }

        public ObservableCollection<DaySchedule> SelectedWeekScheduleDays
        {
            get { return (SelectedComponent as WeekSchedule)?.Days; }
            set
            {
                ((WeekSchedule)selectedComponent).Days = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(SelectedWeekScheduleDays)));
            }
        }

        public ObservableCollection<YearSchedulePart> SelectedYearScheduleParts
        {
            get { return (SelectedComponent as YearSchedule)?.Parts; }
            set
            {
                ((YearSchedule)selectedComponent).Parts = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(SelectedYearScheduleParts)));
            }
        }

        internal bool ConfirmWindowClosing()
        {
            return !HasUnsavedChanges || CheckForSaveAndProceed();
        }

        private static LibraryComponent CreateComponentWithDefaults(Type type)
        {
            var newComponent = (LibraryComponent)Activator.CreateInstance(type);
            var useDefaultsOf = type.GetCustomAttribute<UseDefaultValuesOfAttribute>();
            if (useDefaultsOf != null)
            {
                var source =
                    useDefaultsOf
                    .SourceType
                    .GetProperties()
                    .Select(prop => new { Prop = prop, Default = prop.GetCustomAttribute<DefaultValueAttribute>() })
                    .Where(x => x.Default != null)
                    .Select(x => new { Name = x.Prop.Name, Value = x.Default.Value });
                var matched =
                    type
                    .GetProperties()
                    .Join(
                        source,
                        prop => prop.Name,
                        x => x.Name,
                        (prop, match) => new { Prop = prop, Value = match.Value });
                foreach (var match in matched)
                {
                    match.Prop.SetValue(newComponent, match.Value);
                }
            }
            return newComponent;
        }

        private static Legacy.Library LoadLegacyLibrary(string path)
        {
            using (var reader = new StreamReader(path))
            {
                var serializer = new XmlSerializer(typeof(Legacy.Library));
                return (Legacy.Library)serializer.Deserialize(reader);
            }
        }

        private bool CheckForSaveAndProceed()
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

        private void CreateNewComponent(object componentType)
        {
            var cType = (Type)componentType;
            var newComponent = CreateComponentWithDefaults(cType);
            var vm = new MetadataEditorViewModel()
            {
                Category = newComponent.Category,
                IsCategoryReadOnly = cType.GetCustomAttribute<ImmutableCategoryNameAttribute>() != null,
                ValidateName = name => !CurrentCategorizedComponents.AllComponents.Any(c => c.Name == name && c.GetType() == cType)
            };
            var editWindow = new ComponentMetadataEditWindow() { DataContext = vm };
            var res = editWindow.ShowDialog();
            if (res.HasValue && res.Value)
            {
                newComponent.Name = vm.Name;
                newComponent.Category = vm.Category;
                newComponent.Comments = vm.Comments;
                newComponent.DataSource = vm.DataSource;
                CurrentCategorizedComponents.AddComponent(newComponent);
                newComponent.PropertyChanged += SetUnsavedChangesOnPropertyChange;
                HasUnsavedChanges = true;
            }
        }

        private void DeleteComponent(object component)
        {
            CurrentCategorizedComponents.RemoveComponent((LibraryComponent)component);
            HasUnsavedChanges = true;
        }

        private void DuplicateComponent(object component)
        {
            var cType = component.GetType();
            var newComponent = ((LibraryComponent)component).Duplicate();
            var vm = new MetadataEditorViewModel(newComponent)
            {
                IsCategoryReadOnly = cType.GetCustomAttribute<ImmutableCategoryNameAttribute>() != null,
                ValidateName = name => !CurrentCategorizedComponents.AllComponents.Any(c => c.Name == name && c.GetType() == cType)
            };
            var editWindow = new ComponentMetadataEditWindow() { DataContext = vm };
            var res = editWindow.ShowDialog();
            if (res.HasValue && res.Value)
            {
                newComponent.Name = vm.Name;
                newComponent.Category = vm.Category;
                newComponent.Comments = vm.Comments;
                newComponent.DataSource = vm.DataSource;
                CurrentCategorizedComponents.AddComponent(newComponent);
                newComponent.PropertyChanged += SetUnsavedChangesOnPropertyChange;
                HasUnsavedChanges = true;
            }
        }

        private void EditComponentMetadata(object component)
        {
            var cType = component.GetType();
            var original = (LibraryComponent)component;
            var vm = new MetadataEditorViewModel((LibraryComponent)component)
            {
                IsCategoryReadOnly = cType.GetCustomAttribute<ImmutableCategoryNameAttribute>() != null,
                ValidateName = name =>
                    name == original.Name ||
                    !CurrentCategorizedComponents.AllComponents.Any(c => c.Name == name && c.GetType() == cType)
            };
            var editWindow = new ComponentMetadataEditWindow() { DataContext = vm };
            var res = editWindow.ShowDialog();
            if (res.HasValue && res.Value)
            {
                var oldCategoryName = SelectedComponent.Category;
                SelectedComponent.Name = vm.Name;
                SelectedComponent.Category = vm.Category;
                SelectedComponent.Comments = vm.Comments;
                SelectedComponent.DataSource = vm.DataSource;
                if (oldCategoryName != SelectedComponent.Category)
                {
                    var oldCategory = CurrentCategorizedComponents.Single(cat => cat.CategoryName == oldCategoryName);
                    var newCategory = CurrentCategorizedComponents.Single(cat => cat.CategoryName == SelectedComponent.Category);
                    oldCategory.RemoveComponent(SelectedComponent);
                    newCategory.AddComponent(SelectedComponent);
                }
            }
        }

        private void NewLibrary()
        {
            SetActiveLibrary(new Library());
            CurrentLibraryPath = null;
            HasUnsavedChanges = false;
        }

        private void OpenLibrary()
        {
            try
            {
                if (HasUnsavedChanges && !CheckForSaveAndProceed()) { return; }
                var ofd = new OpenFileDialog()
                {
                    Title = "Select library file",
                    Filter = "Building template libraries|*.xml;*.json"
                };
                var res = ofd.ShowDialog();
                if (res.HasValue && res.Value)
                {
                    // We could use the extension, but there's still no way to distinguish
                    // between legacy XML libraries and new ones, so let's just try
                    // everything.
                    var newLib = default(Core.Library);
                    try
                    {
                        newLib = Core.Library.FromJson(File.ReadAllText(ofd.FileName));
                    }
                    catch { }
                    if (newLib != null)
                    {
                        SetActiveLibrary(Library.Create(newLib), ignoreUnsavedChanges: true);
                        CurrentLibraryPath = ofd.FileName;
                        HasUnsavedChanges = false;
                        return;
                    }
                    try
                    {
                        newLib = Core.Library.FromXml(ofd.FileName);
                    }
                    catch { }
                    if (newLib != null)
                    {
                        SetActiveLibrary(Library.Create(newLib), ignoreUnsavedChanges: true);
                        CurrentLibraryPath = ofd.FileName;
                        HasUnsavedChanges = false;
                        return;
                    }
                    try
                    {
                        var legacy = LoadLegacyLibrary(ofd.FileName);
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
                        SetActiveLibrary(Library.Create(newLib), ignoreUnsavedChanges: true);
                        CurrentLibraryPath = null;
                        HasUnsavedChanges = false;
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
            if (CurrentLibraryPath == null) { SaveAs(); }
            else { SaveTo(CurrentLibraryPath); }
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
                var coreLib = loadedLibrary.ToCoreLibrary();
#if DEBUG
                var orphanCount = coreLib.GetOrphanedComponents().Count();
                if (orphanCount > 0)
                {
                    MessageBox.Show($"{orphanCount} orphaned component(s)");
                }
#endif
                var extension = Path.GetExtension(path);
                if (extension == ".json")
                {
                    var settings = new JsonSerializerSettings()
                    {
                        TypeNameHandling = TypeNameHandling.Objects,
                        Formatting = Newtonsoft.Json.Formatting.Indented
                    };
                    var json = JsonConvert.SerializeObject(loadedLibrary.ToCoreLibrary(), settings);
                    File.WriteAllText(path, json);
                }
                else if (extension == ".xml")
                {
                    using (var fs = new FileStream(path, FileMode.Create))
                    {
                        var serializer = new DataContractSerializer(typeof(Core.Library));
                        serializer.WriteObject(fs, loadedLibrary.ToCoreLibrary());
                    }
                }
                else
                {
                    throw new InvalidOperationException("Unknown library format");
                }
                CurrentLibraryPath = path;
                HasUnsavedChanges = false;
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
            if (IsAnyLibraryLoaded)
            {
                if (HasUnsavedChanges && !ignoreUnsavedChanges && !CheckForSaveAndProceed())
                {
                    return;
                }
                foreach (var component in loadedLibrary.AllComponents)
                {
                    component.PropertyChanged -= SetUnsavedChangesOnPropertyChange;
                }
            }

            loadedLibrary = library;
            foreach (var component in loadedLibrary.AllComponents)
            {
                component.PropertyChanged += SetUnsavedChangesOnPropertyChange;
            }
            PropertyChanged(this, new PropertyChangedEventArgs(string.Empty));
            createNewComponentCommand.RaiseCanExecuteChanged();
            SelectedComponent = null;
        }

        private void SetUnsavedChangesOnPropertyChange(object sender, PropertyChangedEventArgs e)
        {
            HasUnsavedChanges = true;
        }
    }
}
