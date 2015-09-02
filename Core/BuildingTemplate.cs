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
    public class BuildingTemplate : LibraryComponent
    {
        [DataMember]
        public int Lifespan { get; set; }

        #region Construction
        [DataMember]
        public OpaqueConstruction BasementWallConstruction { get; set; }

        [DataMember]
        public OpaqueConstruction FacadeConstruction { get; set; }

        [DataMember]
        public OpaqueConstruction GroundFloorConstruction { get; set; }

        [DataMember]
        public OpaqueConstruction InteriorFloorConstruction { get; set; }

        [DataMember]
        public OpaqueConstruction PartitionConstruction { get; set; }

        [DataMember]
        public double PartitionRatio { get; set; }

        [DataMember]
        public OpaqueConstruction RoofConstruction { get; set; }

        [DataMember]
        public OpaqueConstruction ThermalMassConstruction { get; set; }

        [DataMember]
        public double ThermalMassRatio { get; set; }

        [DataMember]
        public WindowConstruction WindowConstruction { get; set; }
        #endregion

        #region Thermal gains and losses
        [DataMember]
        public double EquipmentGainDensity { get; set; }

        [DataMember]
        public double InfiltrationRate { get; set; }

        [DataMember]
        public double LightingGainDensity { get; set; }

        [DataMember]
        public double OccupancyGainDensity { get; set; }
        #endregion

        #region Natural ventilation
        [DataMember]
        public double NatVentMinTemperatureIn { get; set; }

        [DataMember]
        public double NatVentMinTemperatureOut { get; set; }

        [DataMember, DefaultValue(true)]
        public bool NatVentOn { get; set; }

        [DataMember]
        public double NatVentRate { get; set; }
        #endregion

        #region Heating and cooling
        [DataMember]
        public double CoolingCop { get; set; }

        [DataMember, DefaultValue(true)]
        public bool CoolingOn { get; set; }

        [DataMember]
        public double CoolingSetpoint { get; set; }

        [DataMember]
        public double HeatingCop { get; set; }

        [DataMember, DefaultValue(true)]
        public bool HeatingOn { get; set; }

        [DataMember]
        public double HeatingSetpoint { get; set; }
        #endregion

        #region Mechanical ventilation
        [DataMember, DefaultValue(true)]
        public bool MechVentOn { get; set; }

        [DataMember]
        public double MechVentMinFreshAirPerArea { get; set; }

        [DataMember]
        public double MechVentMinFreshAirPerPerson { get; set; }
        #endregion

        #region Domestic hot water
        [DataMember, DefaultValue(true)]
        public bool DhwOn { get; set; }

        [DataMember]
        public double DhwPeakFlow { get; set; }

        [DataMember]
        public double DhwTemperatureIn { get; set; }

        [DataMember]
        public double DhwTemperatureSupply { get; set; }
        #endregion

        #region Lighting and shading
        [DataMember]
        public bool BlindsOn { get; set; }

        [DataMember, DefaultValue("unset")]
        public string BlindsType { get; set; }

        [DataMember]
        public double LuxMaximum { get; set; }
        
        [DataMember]
        public double LuxTarget { get; set; }
        #endregion
    }
}
