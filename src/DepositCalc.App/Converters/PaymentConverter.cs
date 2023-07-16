using System;
using System.Collections.Generic;
using System.Globalization;
using Avalonia.Data;
using Avalonia.Data.Converters;
using DepositCalc.Core.Models;

namespace DepositCalc.App.Converters;

public class PaymentConverter : IMultiValueConverter
{
    public object Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
    {
        if (values.Count != 2)
            return BindingOperations.DoNothing;
        
        var payment = new Payment
        {
            Date = (values[0] as DateTimeOffset? ?? DateTimeOffset.Now).Date,
            Amount = (decimal)values[1]!
        };

        return payment;
    }
}