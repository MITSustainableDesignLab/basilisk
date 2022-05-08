using Basilisk.Controls.InterfaceModels;
using System;
using System.Globalization;
using System.Windows.Data;

namespace Basilisk.Controls.Converters;

internal class IndependentSettingsConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value switch
        {
            LibraryComponent c => c.SimulationSettings(new NullComponentCoordinator()),
            _ => value
        };
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}
