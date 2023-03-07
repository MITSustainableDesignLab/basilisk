using System.Collections.Generic;

namespace Basilisk.Core.AdvancedStructuralModeling;

public static class LiveLoadingPresetMap
{
    private static readonly Dictionary<LiveLoadingPreset, double> map = new()
    {
        [LiveLoadingPreset.Offices] = 2.4,
        [LiveLoadingPreset.Residential] = 1.92,
        [LiveLoadingPreset.SchoolsClassrooms] = 1.92,
        [LiveLoadingPreset.LibrariesReadingRooms] = 2.87,
        [LiveLoadingPreset.HospitalsLabs] = 2.87,
        [LiveLoadingPreset.AssemblyAreas] = 4.79,
        [LiveLoadingPreset.Restaurants] = 4.79,
        [LiveLoadingPreset.Recreational] = 4.79,
        [LiveLoadingPreset.Retail] = 6.0,
        [LiveLoadingPreset.WarehouseLightStorage] = 6.0,
    };

    public static double? TryGetValue(LiveLoadingPreset preset)
    {
        return map.TryGetValue(preset, out var value)
            ? value
            : null;
    }
}
