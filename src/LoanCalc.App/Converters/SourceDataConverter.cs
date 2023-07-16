using System;
using System.Collections.Generic;
using System.Globalization;
using Avalonia.Data;
using Avalonia.Data.Converters;
using LoanCalc.Core.Models;

namespace LoanCalc.App.Converters;

public class SourceDataConverter : IMultiValueConverter
{
    public object Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
    {
        if (values.Count != 4)
            return BindingOperations.DoNothing;
        
        var data = new SourceData
        {
            Amount = (decimal)values[0]!,
            InterestRate = (decimal)values[1]!,
            Term = (int)(decimal)values[2]!,
            LoanType = (LoanType)(int)values[3]!
        };

        return data;
    }
}