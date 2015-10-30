using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.Core
{
    [DataContract]
    public class ZoneDefinition
    {
        #region Construction
        [DataMember]
        public OpaqueConstruction Basement { get; set; }

        [DataMember]
        public OpaqueConstruction ExteriorFloor { get; set; }

        [DataMember]
        public OpaqueConstruction Facade { get; set; }

        [DataMember]
        public OpaqueConstruction Ground { get; set; }

        [DataMember]
        public OpaqueConstruction InteriorFloor { get; set; }

        [DataMember]
        public OpaqueConstruction Roof { get; set; }

        [DataMember]
        public WindowConstruction Window { get; set; }
        #endregion

        #region Use
        [DataMember]
        public double EquipmentDensity { get; set; }

        [DataMember]
        public YearSchedule EquipmentSchedule { get; set; }

        [DataMember]
        public double LightingDensity { get; set; }

        [DataMember]
        public YearSchedule LightingSchedule { get; set; }

        [DataMember]
        public double OccupancyDensity { get; set; }

        [DataMember]
        public YearSchedule OccupancySchedule { get; set; }
        #endregion

        #region HVAC
        [DataMember]
        public double CoolingCoeffOfPerf { get; set; }

        [DataMember, DefaultValue(true)]
        public bool CoolingOn { get; set; }

        [DataMember]
        public YearSchedule CoolingSchedule { get; set; }

        [DataMember]
        public double CoolingSetpoint { get; set; }

        [DataMember]
        public double HeatingCoeffOfPerf { get; set; }

        [DataMember, DefaultValue(true)]
        public bool HeatingOn { get; set; }

        [DataMember]
        public YearSchedule HeatingSchedule { get; set; }

        [DataMember]
        public double HeatingSetpoint { get; set; }

        [DataMember]
        public double Infiltration { get; set; }

        [DataMember]
        public double MechVentMinFreshAirPerArea { get; set; }

        [DataMember]
        public double MechVentMinFreshAirPerPerson { get; set; }

        [DataMember, DefaultValue(true)]
        public bool MechVentOn { get; set; }

        [DataMember]
        public YearSchedule MechVentSchedule { get; set; }

        [DataMember]
        public double NatVentMinRelHumidity { get; set; }

        [DataMember]
        public double NatVentMinTempIn { get; set; }

        [DataMember]
        public double NatVentMinTempOut { get; set; }

        [DataMember, DefaultValue(true)]
        public bool NatVentOn { get; set; }

        [DataMember]
        public double NatVentRate { get; set; }

        [DataMember]
        public YearSchedule NatVentSchedule { get; set; }
        #endregion

        #region Domestic hot water
        [DataMember, DefaultValue(true)]
        public bool WaterOn { get; set; }

        [DataMember]
        public double WaterPeakFlow { get; set; }

        [DataMember]
        public YearSchedule WaterSchedule { get; set; }

        [DataMember]
        public double WaterTempIn { get; set; }

        [DataMember]
        public double WaterTempSupply { get; set; }
        #endregion

        #region Lighting
        [DataMember]
        public bool BlindsOn { get; set; }

        [DataMember]
        public YearSchedule BlindSchedule { get; set; }

        [DataMember]
        public double BlindSetWatt { get; set; }

        [DataMember]
        public double BlindSetLux { get; set; }

        [DataMember]
        public double BlindTrans { get; set; }

        [DataMember]
        public BlindType BlindType { get; set; }

        [DataMember]
        public double LuxMaximum { get; set; }

        [DataMember]
        public double LuxTarget { get; set; }
        #endregion

        internal IEnumerable<LibraryComponent> ReferencedComponents
        {
            get
            {
                var direct = new LibraryComponent[]
                {
                    Basement,
                    ExteriorFloor,
                    Facade,
                    Ground,
                    InteriorFloor,
                    Roof,
                    Window,
                    EquipmentSchedule,
                    LightingSchedule,
                    OccupancySchedule,
                    CoolingSchedule,
                    HeatingSchedule,
                    MechVentSchedule,
                    NatVentSchedule,
                    WaterSchedule,
                    BlindSchedule
                }.Where(c => c != null);
                return direct.Concat(direct.SelectMany(c => c.ReferencedComponents));
            }
        }
    }
}
