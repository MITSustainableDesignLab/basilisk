using Basilisk.Core.AdvancedStructuralModeling;
using System.ComponentModel;

namespace Basilisk.Controls.InterfaceModels.AdvancedStructuralModeling;

public class FoundationSoilSettings : INotifyPropertyChanged
{
    private bool isValueControlledByPreset;
    private FoundationSoilPreset preset;
    private double selectedValue;

    public FoundationSoilSettings()
    {
        isValueControlledByPreset = true;
        selectedValue = FoundationSoilPresetMap.TryGetValue(default) ?? 0.0;
    }

    public bool IsValueControlledByPreset
    {
        get => isValueControlledByPreset;
        set
        {
            if (isValueControlledByPreset != value)
            {
                isValueControlledByPreset = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsValueControlledByPreset)));
            }
        }
    }

    public FoundationSoilPreset Preset
    {
        get => preset;
        set
        {
            if (preset != value)
            {
                preset = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Preset)));
            }

            if (FoundationSoilPresetMap.TryGetValue(preset) is double newValue)
            {
                SelectedValue = newValue;
                IsValueControlledByPreset = true;
            }
            else
            {
                IsValueControlledByPreset = false;
            }
        }
    }

    public double SelectedValue
    {
        get => selectedValue;
        set
        {
            if (selectedValue != value)
            {
                selectedValue = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedValue)));
            }
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;
}
