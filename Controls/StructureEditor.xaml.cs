using Basilisk.Controls.InterfaceModels;
using Basilisk.Controls.InterfaceModels.AdvancedStructuralModeling;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using PickMaterialFunc = System.Func<Basilisk.Controls.InterfaceModels.IMaterialPickable, System.Collections.Generic.ICollection<Basilisk.Controls.InterfaceModels.LibraryComponent>, bool>;

namespace Basilisk.Controls
{
    /// <summary>
    /// Interaction logic for OpaqueConstructionEditor.xaml
    /// </summary>
    public partial class StructureEditor : UserControl
    {
        public StructureEditor()
        {
            InitializeComponent();
        }

        private void BeginEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            if ((string)e.Column.Header != "Material") { return; }
            var componentWithPickableMaterial = (IMaterialPickable)e.Row.DataContext;
            var success = PickMaterial?.Invoke(componentWithPickableMaterial, MaterialChoices);
            if (success.HasValue && success.Value)
            {
                ((DataGrid)sender).CommitEdit();
            }
            e.Cancel = true;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = (ComboBox)sender;
            comboBox.GetBindingExpression(ComboBox.SelectedItemProperty)?.UpdateSource();
        }

        private void DeleteSelectedRatios(object sender, RoutedEventArgs e)
        {
            var selectedIx = ratiosGrid.SelectedIndex;
            var ratios = Ratios;
            if (selectedIx >= 0 && selectedIx < ratios.Count)
            {
                ratios.RemoveAt(selectedIx);
            }
        }

        public AdvancedStructuralModel AdvancedModel
        {
            get { return (AdvancedStructuralModel)GetValue(AdvancedModelProperty); }
            set { SetValue(AdvancedModelProperty, value); }
        }

        public static readonly DependencyProperty AdvancedModelProperty =
            DependencyProperty.Register(
                nameof(AdvancedModel),
                typeof(AdvancedStructuralModel),
                typeof(StructureEditor));

        public ICollection<SimulationSetting> AssemblyProperties
        {
            get { return (ICollection<SimulationSetting>)GetValue(AssemblyPropertiesProperty); }
            set { SetValue(AssemblyPropertiesProperty, value); }
        }

        public static readonly DependencyProperty AssemblyPropertiesProperty =
            DependencyProperty.Register(
                nameof(AssemblyProperties),
                typeof(ICollection<SimulationSetting>),
                typeof(StructureEditor));

        public ICollection<LibraryComponent> MaterialChoices
        {
            get { return (ICollection<LibraryComponent>)GetValue(MaterialChoicesProperty); }
            set { SetValue(MaterialChoicesProperty, value); }
        }

        public static readonly DependencyProperty MaterialChoicesProperty =
            DependencyProperty.Register(
                nameof(MaterialChoices),
                typeof(ICollection<LibraryComponent>),
                typeof(StructureEditor));

        public ObservableCollection<MassRatios> Ratios
        {
            get { return (ObservableCollection<MassRatios>)GetValue(RatiosProperty); }
            set { SetValue(RatiosProperty, value); }
        }

        public static readonly DependencyProperty RatiosProperty =
            DependencyProperty.Register(
                nameof(Ratios),
                typeof(ObservableCollection<MassRatios>),
                typeof(StructureEditor));

        public PickMaterialFunc PickMaterial
        {
            get { return (PickMaterialFunc)GetValue(PickMaterialProperty); }
            set { SetValue(PickMaterialProperty, value); }
        }

        public static readonly DependencyProperty PickMaterialProperty =
            DependencyProperty.Register(
                nameof(PickMaterial),
                typeof(PickMaterialFunc),
                typeof(StructureEditor));

        public bool UseAdvancedModel
        {
            get { return (bool)GetValue(UseAdvancedModelProperty); }
            set { SetValue(UseAdvancedModelProperty, value); }
        }

        public static readonly DependencyProperty UseAdvancedModelProperty =
            DependencyProperty.Register(
                nameof(UseAdvancedModel),
                typeof(bool),
                typeof(StructureEditor),
                new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
    }
}
