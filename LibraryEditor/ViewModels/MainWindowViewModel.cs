﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Windows;
using System.Windows.Input;

using Microsoft.Win32;

using Newtonsoft.Json;

using Basilisk.Controls;
using Basilisk.Controls.InterfaceModels;

namespace Basilisk.LibraryEditor.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private ComponentCategoryCollection currentlyCategorizedComponents;
        private string currentLibraryPath;
        private ICollection<LibraryComponent> currentlyListedComponents;
        private bool hasUnsavedChanges;
        private Library loadedLibrary;
        private LibraryComponent selectedComponent;
        private ObservableCollection<MaterialLayer> selectedComponentLayers;
        private ObservableCollection<MassRatios> selectedComponentMassRatios;

        public ActionBarViewModel ActionBarViewModel { get; }

        public MainWindowViewModel()
        {
            ActionBarViewModel = new ActionBarViewModel(this);
#if DEBUG
            Instance = this;
#endif
            if (!DesignerProperties.GetIsInDesignMode(new DependencyObject()))
            {
                var args = Environment.GetCommandLineArgs();
                if (args.Length > 1)
                {
                    ActionBarViewModel.OpenLibraryCommand.Execute(args[1]);
                }
            }
        }

#if DEBUG
        private static MainWindowViewModel Instance { get; set; }
#endif

        public event PropertyChangedEventHandler PropertyChanged;

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
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentLibraryPath)));
            }
        }

        public bool HasUnsavedChanges
        {
            get { return hasUnsavedChanges; }
            set
            {
                hasUnsavedChanges = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(HasUnsavedChanges)));
            }
        }

        public Library LoadedLibrary
        {
            get { return loadedLibrary; }
            set
            {
                loadedLibrary = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(String.Empty));
            }
        }

        public IEnumerable<LibraryComponent> AllLoadedComponents => loadedLibrary?.AllComponents;

        public IEnumerable<DaySchedule> LoadedDaySchedules => LoadedSchedules?.Select(s => s as DaySchedule).Where(s => s != null);
        public IEnumerable<WeekSchedule> LoadedWeekSchedules => LoadedSchedules?.Select(s => s as WeekSchedule).Where(s => s != null);
        public IEnumerable<YearSchedule> LoadedYearSchedules => LoadedSchedules?.Select(s => s as YearSchedule).Where(s => s != null);
        public ICollection<LibraryComponent> LoadedGasMaterials => loadedLibrary?.GasMaterials;
        public ICollection<LibraryComponent> LoadedGlazingMaterials => loadedLibrary?.GlazingMaterials;
        public ICollection<LibraryComponent> LoadedOpaqueConstructions => loadedLibrary?.OpaqueConstructions;
        public ICollection<LibraryComponent> LoadedOpaqueMaterials => loadedLibrary?.OpaqueMaterials;
        public ICollection<LibraryComponent> LoadedSchedules => loadedLibrary?.Schedules;
        public ICollection<LibraryComponent> LoadedStructureDefinitions => loadedLibrary?.StructureDefinitions;
        public ICollection<LibraryComponent> LoadedWindowConstructions => loadedLibrary?.WindowConstructions;
        public ICollection<LibraryComponent> LoadedWindowMaterials => LoadedGlazingMaterials?.Concat(LoadedGasMaterials).ToArray();
        public ICollection<LibraryComponent> LoadedZoneConstructions => loadedLibrary?.ZoneConstructions;
        public ICollection<LibraryComponent> LoadedZoneLoads => loadedLibrary?.ZoneLoads;
        public ICollection<LibraryComponent> LoadedZoneConditionings => loadedLibrary?.ZoneConditionings;
        public ICollection<LibraryComponent> LoadedZoneVentilations => loadedLibrary?.ZoneVentilations;
        public ICollection<LibraryComponent> LoadedZoneHotWaters => loadedLibrary?.ZoneHotWaters;
        public ICollection<LibraryComponent> LoadedZones => loadedLibrary?.Zones;
        public ICollection<LibraryComponent> LoadedWindowSettings => loadedLibrary?.WindowSettings;
        public ICollection<LibraryComponent> LoadedTemplates => loadedLibrary?.BuildingTemplates;

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
                    HasUnsavedChanges = true;
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
                    HasUnsavedChanges = true;
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
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedComponent)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedComponentSettings)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedDayScheduleValues)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedWeekScheduleDays)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedYearScheduleParts)));
                ActionBarViewModel.EditSelectedItemMetadataCommand.RaiseCanExecuteChanged();
                ActionBarViewModel.DeleteComponentCommand.RaiseCanExecuteChanged();
                ActionBarViewModel.DuplicateComponentCommand.RaiseCanExecuteChanged();
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
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedComponentLayers)));
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

        public IEnumerable<SimulationSetting> SelectedComponentSettings =>
            SelectedComponent?.SimulationSettings(new ComponentCoordinator(loadedLibrary));

        public IList<double> SelectedDayScheduleValues
        {
            get { return (SelectedComponent as DaySchedule)?.Values; }
            set
            {
                ((DaySchedule)selectedComponent).Values = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedDayScheduleValues)));
            }
        }

        public ObservableCollection<DaySchedule> SelectedWeekScheduleDays
        {
            get { return (SelectedComponent as WeekSchedule)?.Days; }
            set
            {
                ((WeekSchedule)selectedComponent).Days = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedWeekScheduleDays)));
            }
        }

        public ObservableCollection<YearSchedulePart> SelectedYearScheduleParts
        {
            get { return (SelectedComponent as YearSchedule)?.Parts; }
            set
            {
                ((YearSchedule)selectedComponent).Parts = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedYearScheduleParts)));
            }
        }

        internal bool ConfirmWindowClosing()
        {
            return !HasUnsavedChanges || ActionBarViewModel.CheckForSaveAndProceed();
        }

        internal void ImportIntoCurrentLibrary(Library lib)
        {
            LoadedLibrary.Import(lib);
            HasUnsavedChanges = true;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
        }
        
        internal void SetUnsavedChangesOnPropertyChange(object sender, PropertyChangedEventArgs e)
        {
            HasUnsavedChanges = true;
        }
    }
}
