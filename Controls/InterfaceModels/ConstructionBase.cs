using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;

namespace Basilisk.Controls.InterfaceModels
{
    [UseDefaultValuesOf(typeof(Core.ConstructionBase))]
    public abstract class ConstructionBase : LibraryComponent
    {
        private ObservableCollection<MaterialLayer> layers;

        static ConstructionBase()
        {
            Mapper
                .CreateMap<Core.ConstructionBase, ConstructionBase>()
                .IncludeBase<Core.LibraryComponent, LibraryComponent>();
        }

        [SimulationSetting(DisplayName = "Assembly Carbon")]
        public double AssemblyCarbon { get; set; }

        [SimulationSetting(DisplayName = "Assembly Cost")]
        public double AssemblyCost { get; set; }

        [SimulationSetting(DisplayName = "Assembly Energy")]
        public double AssemblyEnergy { get; set; }

        [SimulationSetting(DisplayName = "Disassembly Carbon")]
        public double DisassemblyCarbon { get; set; }

        [SimulationSetting(DisplayName = "Disassembly Cost")]
        public double DisassemblyCost { get; set; }

        public ObservableCollection<MaterialLayer> Layers
        {
            get { return layers; }
            set
            {
                if (layers == value) { return; }
                if (layers != null)
                {
                    foreach (var layer in layers) { layer.PropertyChanged -= OnLayersChanged; }
                    layers.CollectionChanged -= OnLayersChanged;
                }
                layers = value;
                foreach (var layer in layers) { layer.PropertyChanged += OnLayersChanged; }
                layers.CollectionChanged += OnLayersChanged;
                RaisePropertyChanged(this, nameof(Layers));
            }
        }

        protected void CopyBasePropertiesFrom(ConstructionBase source)
        {
            AssemblyCarbon = source.AssemblyCarbon;
            AssemblyCost = source.AssemblyCost;
            AssemblyEnergy = source.AssemblyEnergy;
            DisassemblyCarbon = source.DisassemblyCarbon;
            DisassemblyCost = source.DisassemblyCost;
            var layers =
                source
                .Layers
                .Select(layer => new MaterialLayer()
                {
                    Material = layer.Material,
                    Thickness = layer.Thickness
                });
            Layers = new ObservableCollection<MaterialLayer>(layers);
            CopyBasePropertiesFrom((LibraryComponent)source);
        }

        private void OnLayersChanged(object sender, EventArgs e)
        {
            RaisePropertyChanged(sender, nameof(Layers));
        }
    }
}
