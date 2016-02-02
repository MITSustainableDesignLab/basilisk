using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;


using Basilisk.Core;
using Basilisk.Controls.Attributes;

namespace Basilisk.Controls.InterfaceModels
{
    [UseDefaultValuesOf(typeof(Core.WindowSettings))]
    [DisplayName("window settings")]
    [ComponentNamespace]
    public class WindowSettings : LibraryComponent
    {
        [SimulationSetting]
        public WindowType Type { get; set; }

        [SimulationSetting]
        public WindowConstruction Construction { get; set; }

        [SimulationSetting(DisplayName = "Operable area")]
        public double OperableArea { get; set; }

        [SimulationSetting(DisplayName = "Shading is used")]
        public bool IsShadingSystemOn { get; set; }

        [SimulationSetting(DisplayName = "Shading system availability schedule")]
        public YearSchedule ShadingSystemAvailabilitySchedule { get; set; }

        [SimulationSetting(DisplayName = "Shading system setpoint (W)")]
        public double ShadingSystemSetpoint { get; set; }

        [SimulationSetting(DisplayName = "Shading system transmittance")]
        public double ShadingSystemTransmittance { get; set; }

        [SimulationSetting(DisplayName = "Shading system type")]
        public ShadingType ShadingSystemType { get; set; }

        [SimulationSetting(DisplayName = "Zone mixing")]
        public bool IsZoneMixingOn { get; set; }

        [SimulationSetting(DisplayName = "Zone mixing availability schedule")]
        public YearSchedule ZoneMixingAvailabilitySchedule { get; set; }

        [SimulationSetting(DisplayName = "Zone mixing delta temperature")]
        public double ZoneMixingDeltaTemperature { get; set; }

        [SimulationSetting(DisplayName = "Zone mixing flow rate")]
        public double ZoneMixingFlowRate { get; set; }

        [SimulationSetting(DisplayName = "Virtual partition")]
        public bool IsVirtualPartition { get; set; }

        [SimulationSetting]
        public double AfnDischargeC { get; set; }

        [SimulationSetting]
        public double AfnTempSetpoint { get; set; }

        [SimulationSetting]
        public YearSchedule AfnWindowAvailability { get; set; }

        public override IEnumerable<LibraryComponent> AllReferencedComponents
        {
            get
            {
                var direct = new LibraryComponent[]
                {
                    ShadingSystemAvailabilitySchedule,
                    ZoneMixingAvailabilitySchedule,
                    AfnWindowAvailability,
                    Construction
                }.Where(d => d != null);
                return
                    direct
                    .Concat(direct.SelectMany(d => d.AllReferencedComponents))
                    .Distinct();
            }
        }

        public override bool DirectlyReferences(LibraryComponent component) =>
            component == ShadingSystemAvailabilitySchedule ||
            component == ZoneMixingAvailabilitySchedule ||
            component == AfnWindowAvailability ||
            component == Construction;

        public override LibraryComponent Duplicate()
        {
            var res = new WindowSettings()
            {
                Construction = Construction,
                ShadingSystemAvailabilitySchedule = ShadingSystemAvailabilitySchedule,
                ZoneMixingAvailabilitySchedule = ZoneMixingAvailabilitySchedule,
                AfnWindowAvailability = AfnWindowAvailability
            };
            CopyNonReferenceProperties(res, this);
            res.CopyBasePropertiesFrom(this);
            return res;
        }

        public override void OverwriteWith(LibraryComponent other, ComponentCoordinator coord)
        {
            var c = (WindowSettings)other;
            CopyNonReferenceProperties(this, c);
            Construction = coord.GetWithSameName(c.Construction);
            ShadingSystemAvailabilitySchedule = coord.GetWithSameName(c.ShadingSystemAvailabilitySchedule);
            ZoneMixingAvailabilitySchedule = coord.GetWithSameName(c.ZoneMixingAvailabilitySchedule);
            AfnWindowAvailability = coord.GetWithSameName(c.AfnWindowAvailability);
            CopyBasePropertiesFrom(c);
        }

        private static void CopyNonReferenceProperties(WindowSettings to, WindowSettings from)
        {
            to.Type = from.Type;
            to.OperableArea = from.OperableArea;
            to.IsShadingSystemOn = from.IsShadingSystemOn;
            to.ShadingSystemSetpoint = from.ShadingSystemSetpoint;
            to.ShadingSystemTransmittance = from.ShadingSystemTransmittance;
            to.ShadingSystemType = from.ShadingSystemType;
            to.IsZoneMixingOn = from.IsZoneMixingOn;
            to.ZoneMixingDeltaTemperature = from.ZoneMixingDeltaTemperature;
            to.ZoneMixingFlowRate = from.ZoneMixingFlowRate;
            to.IsVirtualPartition = from.IsVirtualPartition;
            to.AfnDischargeC = from.AfnDischargeC;
            to.AfnTempSetpoint = from.AfnTempSetpoint;
        }
    }
}
