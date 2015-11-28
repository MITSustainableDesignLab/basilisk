using Basilisk.Controls.Attributes;

namespace Basilisk.Controls.InterfaceModels
{
    [UseDefaultValuesOf(typeof(Core.BuildingTemplate))]
    [DisplayName("building template")]
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

        [SimulationSetting]
        public WindowSettings Windows { get; set; }

        public override bool DirectlyReferences(LibraryComponent component) =>
            component == Core ||
            component == Perimeter ||
            component == Structure ||
            component == Windows;

        public override LibraryComponent Duplicate()
        {
            var res = new BuildingTemplate()
            {
                Core = Core,
                Perimeter = Perimeter,
                Structure = Structure,
                PartitionRatio = PartitionRatio,
                Lifespan = Lifespan,
                Windows = Windows
            };
            res.CopyBasePropertiesFrom(this);
            return res;
        }
    }
}
