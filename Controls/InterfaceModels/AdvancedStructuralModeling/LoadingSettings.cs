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

    public LiveLoadingPreset LoadingPreset
    {
        get => loadingPreset;
        set
        {
            if (loadingPreset != value)
            {
                loadingPreset = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(LoadingPreset)));

                if (LiveLoadingPresetMap.TryGetValue(loadingPreset) is double newValue)
                {
                    LoadingValue = newValue;
                    IsLoadingValueControlledByPreset = true;
                }
                else
                {
                    IsLoadingValueControlledByPreset = false;
                }
            }
        }
    }

    public double LoadingValue
    {
        get => loadingValue;
        set
        {
            if (loadingValue != value)
            {
                loadingValue = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(LoadingValue)));
            }   
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;
}
