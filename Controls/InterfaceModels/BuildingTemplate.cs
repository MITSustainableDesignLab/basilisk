using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.Controls.InterfaceModels
{
    [UseDefaultValuesOf(typeof(Core.BuildingTemplate))]
    public class BuildingTemplate : LibraryComponent
    {
        [SimulationSetting(DisplayName = "Core zone type")]
        public ZoneDefinition Core { get; set; }

        [SimulationSetting(DisplayName = "Perimeter zone type")]
        public ZoneDefinition Perimeter { get; set; }

        [SimulationSetting]
        public StructureInformation Structure { get; set; }

        [SimulationSetting(DisplayName = "Partition ratio")]
        public double PartitionRatio { get; set; }

        [SimulationSetting]
        public int Lifespan { get; set; }

        public override LibraryComponent Duplicate()
        {
            var res = new BuildingTemplate()
            {
                Core = Core,
                Perimeter = Perimeter,
                Structure = Structure,
                PartitionRatio = PartitionRatio,
                Lifespan = Lifespan
            };
            res.CopyBasePropertiesFrom(this);
            return res;
        }
    }
}
