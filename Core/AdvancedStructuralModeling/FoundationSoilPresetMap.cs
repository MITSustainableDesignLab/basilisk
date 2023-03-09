using System.Collections.Generic;

namespace Basilisk.Core.AdvancedStructuralModeling;

public static class FoundationSoilPresetMap
{
    private static readonly Dictionary<FoundationSoilPreset, double> map = new()
    {
        [FoundationSoilPreset.Clay] = 72,
        [FoundationSoilPreset.Gravel] = 97,
        [FoundationSoilPreset.Sand] = 144
    };

    public static double? TryGetValue(FoundationSoilPreset preset)
    {
        return map.TryGetValue(preset, out var value)
            ? value
            : null;
    }
}
