using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace DepositCalc.App.Models;

public class DataGridHeightConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var windowHeight = (double)value!;
        return windowHeight - 260;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}