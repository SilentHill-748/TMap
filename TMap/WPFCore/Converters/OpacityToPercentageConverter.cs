using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace TMap.WPFCore.Converters;

public class OpacityToPercentageConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is { } && value is double opacity)
        {
            return opacity * 100;
        }

        return 0;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return DependencyProperty.UnsetValue;
        //if (value is { } && value is string strValue)
        //{
        //    return double.Parse(strValue) / 100;
        //}

        //return 0;
    }
}
