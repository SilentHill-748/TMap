using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace TMap.CustomControls.Converters;

internal class AddLeftPaddingConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (!(value is Thickness padding))
        {
            return value;
        }

        if (!double.TryParse(parameter.ToString(), out double amount))
        {
            return value;
        }

        padding.Left += amount;
        padding.Right += amount;
        
        return padding;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return DependencyProperty.UnsetValue;
    }
}
