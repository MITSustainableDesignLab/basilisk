using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace Basilisk.Controls
{
    public class DoubleSequenceSettingConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof(string)) { throw new NotImplementedException(); }
            if (value == null) { return string.Empty; }
            var list = value as IEnumerable<double>;
            if (list == null) { return value; }
            return String.Join(",", list.Select(x => x.ToString()));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                if (targetType == typeof(double[]))
                {
                    return new double[0];
                }
                else if (typeof(IEnumerable<double>).IsAssignableFrom(targetType))
                {
                    return new List<double>();
                }
            }
            if (!typeof(IEnumerable<double>).IsAssignableFrom(targetType)) { return value; }
            var csv = value as string;
            if (csv == null)
            {
                throw new ArgumentException($"A {nameof(DoubleSequenceSettingConverter)} cannot convert back to double sequences values that are not strings.", nameof(value));
            }
            var parsed = csv.Split(',').Select(Double.Parse);
            if (targetType == typeof(double[]))
            {
                return parsed.ToArray();
            }
            else
            {
                return parsed.ToList();
            }
        }
    }
}
