using Basilisk.Core.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Basilisk.Core.AdvancedStructuralModeling;

[JsonConverter(typeof(StringEnumConverter))]
public enum LiveLoadingPreset
{
    [DisplayText("Offices_2.4_kPA")]
    Offices = 0,

    [DisplayText("Residential_1.92_kPa")]
    Residential,

    [DisplayText("Schools_Classrooms_1.92_kPa")]
    SchoolsClassrooms,

    [DisplayText("Libraries_Reading_Rooms_2.87_kPa")]
    LibrariesReadingRooms,

    [DisplayText("Hospitals_Labs_2.87_kPa")]
    HospitalsLabs,

    [DisplayText("Assembly_Areas_4.79_kPa")]
    AssemblyAreas,

    [DisplayText("Restaurants_4.79_kPa")]
    Restaurants,

    [DisplayText("Recreational_4.79_kPa")]
    Recreational,

    [DisplayText("Retail_6_kPa")]
    Retail,

    [DisplayText("Warehouse_Light_Storage_6_kPa")]
    WarehouseLightStorage,

    [DisplayText("Libraries_Stack_Rooms_7.18_kPa")]
    LibrariesStackRooms,

    [DisplayText("Warehouse_Heavy_Storage_11.97_kPa")]
    WarehouseHeavyStorage,

    [DisplayText("Other")]
    Other
}
