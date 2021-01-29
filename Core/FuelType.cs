namespace Basilisk.Core
{
    /// <summary>
    /// Fuel types taken from EnergyPlus 9.2 .idd file for OtherEquipment.
    /// </summary>
    public enum FuelType
    {
        None,
        Electricity,
        NaturalGas,
        PropaneGas,
        FuelOil1,
        FuelOil2,
        Diesel,
        Gasoline,
        Coal,
        OtherFuel1,
        OtherFuel2,
        Steam,
        DistrictHeating,
        DistrictCooling,
    }
}