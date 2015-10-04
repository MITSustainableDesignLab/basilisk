namespace Basilisk.Legacy
{
    public class BuildingTemplate
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public int LifeSpan { get; set; }
        public string DataSource { get; set; }
        public string Comments { get; set; }

        public string FacadeWl { get; set; }
        public string RoofFl { get; set; }
        public string GroundFl { get; set; }
        public string InteriorFl { get; set; }
        public string ExteriorFl { get; set; }
        public string BasementWl { get; set; }
        public string Glazing { get; set; }

        public string StructureTy { get; set; }
        public string PartitionWl { get; set; }
        public double PartRatio { get; set; }
        public string MassConst { get; set; }
        public double MassRatio { get; set; }

        public double OccupDnst { get; set; }
        public double EquipDnst { get; set; }
        public double LightDnst { get; set; }
        public string OccupSchd { get; set; }
        public string EquipSchd { get; set; }
        public string LightSchd { get; set; }

        public double Infiltration { get; set; }

        public double NatVent { get; set; }
        public string NatVentSchd { get; set; }
        public bool NatVentOn { get; set; }

        public bool HeatingOn { get; set; }
        public bool CoolingOn { get; set; }

        public string HeatingSchd { get; set; }
        public string CoolingSchd { get; set; }

        public double HeatingSet { get; set; }
        public double CoolingSet { get; set; }

        public double MechVentMinFreshAirPerArea { get; set; }
        public double MechVentMinFreshAirPerson { get; set; }
        public bool MechVentOn { get; set; }

        public bool WaterOn { get; set; }
        public string WaterSchd { get; set; }
        public double WaterPeakFlow { get; set; }
        public double WaterTempIn { get; set; }
        public double WaterTempSup { get; set; }

        public double LuxTarget { get; set; }
        public double LuxMaximum { get; set; }

        public bool BlindOn { get; set; }
        public double BlindTrns { get; set; }
        public double BlindSetWatt { get; set; }
        public double BlindSetLux { get; set; }
        public BlindType BlindT { get; set; }
        public string BlindSchd { get; set; }

        public string MechVentSchd { get; set; }
        public double HeatingCoP { get; set; }
        public double CoolingCoP { get; set; }
        public double NatVMinTin { get; set; }
        public double NatVMinTout { get; set; }
        public double NatVMinRH { get; set; }
    }
}
