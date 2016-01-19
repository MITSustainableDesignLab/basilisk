using System.Collections.ObjectModel;

using Basilisk.Controls.Attributes;

namespace Basilisk.Controls.InterfaceModels
{
    [UseDefaultValuesOf(typeof(Core.StructureInformation))]
    [DisplayName("structure definition")]
    public class StructureInformation : ConstructionBase
    {
        public ObservableCollection<MassRatios> MassRatios { get; set; } = new ObservableCollection<MassRatios>();

        public override bool DirectlyReferences(LibraryComponent component) =>
            false;

        public override LibraryComponent Duplicate()
        {
            var res = new StructureInformation()
            {
                AssemblyCarbon = AssemblyCarbon,
                AssemblyEnergy = AssemblyEnergy,
                DisassemblyCarbon = DisassemblyCarbon,
                DisassemblyEnergy = DisassemblyEnergy
            };
            res.CopyBasePropertiesFrom(this);
            return res;
        }
    }
}
