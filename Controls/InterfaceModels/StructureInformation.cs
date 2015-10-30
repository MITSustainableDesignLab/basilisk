using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.Controls.InterfaceModels
{
    [UseDefaultValuesOf(typeof(Core.StructureInformation))]
    public class StructureInformation : ConstructionBase
    {
        public ObservableCollection<MassRatios> MassRatios { get; set; }

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
