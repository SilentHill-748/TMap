using System;
using System.Globalization;
using System.Windows.Data;

namespace TMap.WPFCore.Converters;

internal class MetersToPixelsConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value.Equals(0))
            return null!;

        if (double.TryParse($"{value}", out double pixels))
            return $"{pixels / 100}";

        return double.NaN;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        _ = double.TryParse((string)value, out double meters);
        
        return meters * 100;
    }
}
