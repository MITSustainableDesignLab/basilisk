using Basilisk.Core.AdvancedStructuralModeling;
using System.ComponentModel;

namespace Basilisk.Controls.InterfaceModels.AdvancedStructuralModeling;

public class LoadingSettings : INotifyPropertyChanged
{
    private bool isLoadingValueControlledByPreset;
    private LiveLoadingPreset loadingPreset;
    private double loadingValue;

    public LoadingSettings()
    {
        isLoadingValueControlledByPreset = true;
        loadingValue = LiveLoadingPresetMap.TryGetValue(default) ?? 0.0;
    }

    public bool IsLoadingValueControlledByPreset
    {
        get => isLoadingValueControlledByPreset;
        set
        {
            if (isLoadingValueControlledByPreset != value)
            {
                isLoadingValueControlledByPreset = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsLoadingValueControlledByPreset)));
            }
        }
    }

    public LiveLoadingPreset LiveLoadingPreset
    {
        get => loadingPreset;
        set
        {
            if (loadingPreset != value)
            {
                loadingPreset = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(LiveLoadingPreset)));

                if (LiveLoadingPresetMap.TryGetValue(loadingPreset) is double newValue)
                {
                    LiveLoadingValue = newValue;
                    IsLoadingValueControlledByPreset = true;
                }
                else
                {
                    IsLoadingValueControlledByPreset = false;
                }
            }
        }
    }

    public double LiveLoadingValue
    {
        get => loadingValue;
        set
        {
            if (loadingValue != value)
            {
                loadingValue = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(LiveLoadingValue)));
            }   
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;
}
