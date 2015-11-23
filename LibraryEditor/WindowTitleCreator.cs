using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Basilisk.LibraryEditor
{
    public class WindowTitleCreator : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length != 3)
            {
                throw new ArgumentException("A WindowTitleCreator requires a boolean indicating whether a library is loaded, the current library path (or null), and a boolean indicating unsaved changes.");
            }
            var isAnyLibraryLoaded = (bool)values[0];
            var currentLibraryPath = (string)values[1];
            var unsavedChanges = (bool)values[2];

            var prefix = "Template library editor - ";
            var payload =
                !isAnyLibraryLoaded ? "(no library loaded)" :
                currentLibraryPath == null ? "(new library)" :
                currentLibraryPath;
            var indicateChanges = currentLibraryPath != null && unsavedChanges;
            return prefix + payload + (indicateChanges ? "*" : String.Empty);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
