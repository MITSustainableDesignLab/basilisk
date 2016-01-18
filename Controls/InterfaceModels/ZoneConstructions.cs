using Basilisk.Controls.Attributes;

namespace Basilisk.Controls.InterfaceModels
{
    [UseDefaultValuesOf(typeof(Core.ZoneConstructions))]
    [DisplayName("zone constructions")]
    [ComponentNamespace]
    public class ZoneConstructions : LibraryComponent
    {
        [SimulationSetting]
        public OpaqueConstruction Facade { get; set; }

        [SimulationSetting]
        public OpaqueConstruction Ground { get; set; }

        [SimulationSetting]
        public OpaqueConstruction Partition { get; set; }

        [SimulationSetting]
        public OpaqueConstruction Roof { get; set; }

        [SimulationSetting]
        public OpaqueConstruction Slab { get; set; }

        [SimulationSetting(DisplayName = "Facade is adiabatic")]
        public bool IsFacadeAdiabatic { get; set; }

        [SimulationSetting(DisplayName = "Ground is adiabatic")]
        public bool IsGroundAdiabatic { get; set; }

        [SimulationSetting(DisplayName = "Partition is adiabatic")]
        public bool IsPartitionAdiabatic { get; set; }

        [SimulationSetting(DisplayName = "Roof is adiabatic")]
        public bool IsRoofAdiabatic { get; set; }

        [SimulationSetting(DisplayName = "Slab is adiabatic")]
        public bool IsSlabAdiabatic { get; set; }

        public override bool DirectlyReferences(LibraryComponent component) =>
            Facade == component ||
            Ground == component ||
            Partition == component ||
            Roof == component ||
            Slab == component;

        public override LibraryComponent Duplicate()
        {
            var res = new ZoneConstructions()
            {
                Facade = Facade,
                Ground = Ground,
                Partition = Partition,
                Roof = Roof,
                Slab = Slab,
                IsFacadeAdiabatic = IsFacadeAdiabatic,
                IsGroundAdiabatic = IsGroundAdiabatic,
                IsPartitionAdiabatic = IsPartitionAdiabatic,
                IsRoofAdiabatic = IsRoofAdiabatic,
                IsSlabAdiabatic = IsSlabAdiabatic
            };
            res.CopyBasePropertiesFrom(this);
            return res;
        }
    }
}
