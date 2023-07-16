using System;
using System.Collections.Generic;
using System.Globalization;
using Avalonia.Data;
using Avalonia.Data.Converters;
using DepositCalc.Core.Models;

namespace DepositCalc.App.Converters;

public class SourceDataConverter : IMultiValueConverter
{
    public object Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
    {
        if (values.Count != 11)
            return BindingOperations.DoNothing;

        var data = new SourceData
        {
            Amount = (decimal)values[0]!,
            Term = (int)(decimal)values[1]!,
            StartDate = (values[2] as DateTimeOffset? ?? DateTimeOffset.Now).Date,
            InterestRate = (decimal)values[3]!,
            TaxRate = (decimal)values[4]!,
            Capitalization = (bool)values[5]!,
            CapitalizationPeriodicity = (Periodicity)(int)values[6]!,
            PeriodicalAdditions = new PeriodicalPayment
            {
                Periodicity = (Periodicity)(int)values[7]!,
                Amount = (decimal)values[8]!
            },
            PeriodicalWithdrawals = new PeriodicalPayment
            {
                Periodicity = (Periodicity)(int)values[9]!,
                Amount = (decimal)values[10]!
            }
        };

        return data;
    }
}