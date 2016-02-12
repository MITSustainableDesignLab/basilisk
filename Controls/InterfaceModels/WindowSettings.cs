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
        [DefaultValue(WindowType.External)]
        public WindowType Type { get; set; } = WindowType.External;

        [SimulationSetting]
        public WindowConstruction Construction { get; set; }

        [SimulationSetting(DisplayName = "Operable area")]
        public double OperableArea { get; set; }

        [SimulationSetting(DisplayName = "Shading is used")]
        public bool IsShadingSystemOn { get; set; }

        [SimulationSetting(DisplayName = "Shading system availability schedule")]
        public YearSchedule ShadingSystemAvailabilitySchedule { get; set; }

        [SimulationSetting(DisplayName = "Shading system setpoint", Units = "W/m2")]
        [DefaultValue(180)]
        public double ShadingSystemSetpoint { get; set; }

        [SimulationSetting(DisplayName = "Shading system transmittance")]
        [DefaultValue(0.5)]
        public double ShadingSystemTransmittance { get; set; } = 0.5;

        [SimulationSetting(DisplayName = "Shading system type")]
        public ShadingType ShadingSystemType { get; set; }

        [SimulationSetting(DisplayName = "Zone mixing")]
        public bool IsZoneMixingOn { get; set; }

        [SimulationSetting(DisplayName = "Zone mixing availability schedule")]
        public YearSchedule ZoneMixingAvailabilitySchedule { get; set; }

        [SimulationSetting(DisplayName = "Zone mixing delta temperature")]
        [DefaultValue(2.0)]
        public double ZoneMixingDeltaTemperature { get; set; } = 2.0;

        [SimulationSetting(DisplayName = "Zone mixing flow rate", Units = "m3/2")]
        [DefaultValueAttribute(0.001)]
        public double ZoneMixingFlowRate { get; set; } = 0.001;

        [SimulationSetting(DisplayName = "Virtual partition")]
        public bool IsVirtualPartition { get; set; }

        [SimulationSetting(DisplayName = "Airflow network discharge coefficient")]
        [DefaultValue(0.65)]
        public double AfnDischargeC { get; set; } = 0.65;

        [SimulationSetting(DisplayName = "Airflow network temperature setpoint", Units = "degC")]
        [DefaultValue(20)]
        public double AfnTempSetpoint { get; set; } = 20;

        [SimulationSetting(DisplayName = "Airflow network window availability")]
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
