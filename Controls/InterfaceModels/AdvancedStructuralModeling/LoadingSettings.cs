using System.ComponentModel;

namespace Basilisk.Controls.InterfaceModels.AdvancedStructuralModeling;

public class LoadingSettings : INotifyPropertyChanged
{
    private bool isLoadingValueEditable;
    private double loadingValue;

    public bool IsLoadingValueEditable
    {
        get => isLoadingValueEditable;
        set
        {
            if (isLoadingValueEditable != value)
            {
                isLoadingValueEditable = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsLoadingValueEditable)));
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
