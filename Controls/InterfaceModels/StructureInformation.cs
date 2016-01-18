using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;

using Basilisk.Controls.Attributes;

namespace Basilisk.Controls.InterfaceModels
{
    [UseDefaultValuesOf(typeof(Core.StructureInformation))]
    [DisplayName("structure definition")]
    [ComponentNamespace]
    public class StructureInformation : ConstructionBase
    {
        public ObservableCollection<MassRatios> MassRatios { get; set; } = new ObservableCollection<MassRatios>();

        public override IEnumerable<LibraryComponent> AllReferencedComponents =>
            MassRatios
            .Where(mr => mr.Material != null)
            .SelectMany(mr => mr.Material.AllReferencedComponents)
            .Distinct();

        public override bool DirectlyReferences(LibraryComponent component) =>
            false;

        public override LibraryComponent Duplicate()
        {
            var res = new StructureInformation()
            {
                AssemblyCarbon = AssemblyCarbon,
                AssemblyEnergy = AssemblyEnergy,
                DisassemblyCarbon = DisassemblyCarbon,
                DisassemblyEnergy = DisassemblyEnergy,
                MassRatios = new ObservableCollection<MassRatios>(MassRatios.Select(mr => mr.Duplicate()))
            };
            res.CopyBasePropertiesFrom(this);
            return res;
        }
    }
}
