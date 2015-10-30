using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using Basilisk.Controls.InterfaceModels;

using PickMaterialFunc = System.Func<Basilisk.Controls.InterfaceModels.IMaterialPickable, System.Collections.Generic.ICollection<Basilisk.Controls.InterfaceModels.LibraryComponent>, bool>;

namespace Basilisk.Controls
{
    /// <summary>
    /// Interaction logic for OpaqueConstructionEditor.xaml
    /// </summary>
    public partial class ConstructionEditor : UserControl
    {
        public ConstructionEditor()
        {
            InitializeComponent();
        }

        private void BeginEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            if ((string)e.Column.Header != "Material") { return; }
            var layer = (MaterialLayer)e.Row.DataContext;
            var success = PickMaterial?.Invoke(layer, LayerMaterialChoices);
            if (success.HasValue && success.Value)
            {
                ((DataGrid)sender).CommitEdit();
            }
            e.Cancel = true;
        }

        private void DeleteSelectedLayer(object sender, RoutedEventArgs e)
        {
            var selectedIx = layersGrid.SelectedIndex;
            var layers = Layers;
            if (selectedIx >= 0 && selectedIx < layers.Count)
            {
                layers.RemoveAt(selectedIx);
            }
        }

        private void MoveSelectedLayerDown(object sender, RoutedEventArgs e)
        {
            var selectedIx = layersGrid.SelectedIndex;
            var layers = Layers;
            if (selectedIx >= 0 && selectedIx < layers.Count - 1)
            {
                var layer = layers.ElementAt(selectedIx);
                layers.RemoveAt(selectedIx);
                layers.Insert(selectedIx + 1, layer);
                layersGrid.SelectedIndex = selectedIx + 1;
            }
        }

        private void MoveSelectedLayerUp(object sender, RoutedEventArgs e)
        {
            var selectedIx = layersGrid.SelectedIndex;
            var layers = Layers;
            if (selectedIx >= 1 && selectedIx < layers.Count)
            {
                var layer = layers.ElementAt(selectedIx);
                layers.RemoveAt(selectedIx);
                layers.Insert(selectedIx - 1, layer);
                layersGrid.SelectedIndex = selectedIx - 1;
            }
        }

        public ICollection<LibraryComponent> LayerMaterialChoices
        {
            get { return (ICollection<LibraryComponent>)GetValue(LayerMaterialChoicesProperty); }
            set { SetValue(LayerMaterialChoicesProperty, value); }
        }

        public static readonly DependencyProperty LayerMaterialChoicesProperty =
            DependencyProperty.Register(
                nameof(LayerMaterialChoices),
                typeof(ICollection<LibraryComponent>),
                typeof(ConstructionEditor));

        public ObservableCollection<MaterialLayer> Layers
        {
            get { return (ObservableCollection<MaterialLayer>)GetValue(LayersProperty); }
            set { SetValue(LayersProperty, value); }
        }

        public static readonly DependencyProperty LayersProperty =
            DependencyProperty.Register(
                nameof(Layers),
                typeof(ObservableCollection<MaterialLayer>),
                typeof(ConstructionEditor));

        public PickMaterialFunc PickMaterial
        {
            get { return (PickMaterialFunc)GetValue(PickMaterialProperty); }
            set { SetValue(PickMaterialProperty, value); }
        }

        public static readonly DependencyProperty PickMaterialProperty =
            DependencyProperty.Register(
                nameof(PickMaterial),
                typeof(PickMaterialFunc),
                typeof(ConstructionEditor));

        public ICollection<SimulationSetting> Settings
        {
            get { return (ICollection<SimulationSetting>)GetValue(SettingsProperty); }
            set { SetValue(SettingsProperty, value); }
        }

        public static readonly DependencyProperty SettingsProperty =
            DependencyProperty.Register(
                nameof(Settings),
                typeof(ICollection<SimulationSetting>),
                typeof(ConstructionEditor));
    }
}
