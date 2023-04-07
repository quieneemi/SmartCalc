using DepositCalc.Core.Interfaces;
using DepositCalc.Core.Models;

namespace DepositCalc.Core;

public class DepositCalcService : IDepositCalcService
{
    public ResultData Calculate(SourceData data)
    {
        var currentDate = data.StartDate;
        var paymentDate = UpdatePaymentDate(currentDate, data.CapitalizationPeriodicity);
        var additionDate = UpdatePaymentDate(currentDate, data.PeriodicalAdditions.Periodicity);
        var withdrawalDate = UpdatePaymentDate(currentDate, data.PeriodicalWithdrawals.Periodicity);
        var endDate = currentDate.AddMonths(data.Term);

        var interests = 0m;
        var amount = data.Amount;
        var payment = 0m;

        while (currentDate != endDate)
        {
            var daysInYear = DateTime.IsLeapYear(currentDate.Year) ? 366 : 365;
            var rate = data.InterestRate / daysInYear;

            if (data.PeriodicalAdditions.Amount > 0 && currentDate == additionDate)
            {
                amount += data.PeriodicalAdditions.Amount;
                additionDate = UpdatePaymentDate(additionDate, data.PeriodicalAdditions.Periodicity);
            }
            if (data.PeriodicalWithdrawals.Amount > 0 && currentDate == withdrawalDate)
            {
                amount -= data.PeriodicalWithdrawals.Amount;
                withdrawalDate = UpdatePaymentDate(withdrawalDate, data.PeriodicalWithdrawals.Periodicity);
            }

            amount += data.Additions
                .Where(x => x.Date == currentDate)
                .Sum(addition => addition.Amount);

            amount = data.Withdrawals
                .Where(x => x.Date == currentDate)
                .Aggregate(amount, (current, withdrawal) => current - withdrawal.Amount);

            if (amount <= 0) break;

            var temp = amount * rate;
            interests += temp;
            payment += temp;

            if (data.Capitalization && currentDate == paymentDate)
            {
                amount += payment;
                payment = 0;
                paymentDate = UpdatePaymentDate(paymentDate, data.CapitalizationPeriodicity);
            }

            currentDate = currentDate.AddDays(1);
        }

        if (data.Capitalization) amount += payment;

        var taxes = interests * data.TaxRate;
        interests -= taxes;

        return new ResultData
        {
            Interests = interests,
            Taxes = taxes,
            Amount = amount
        };
    }
    
    private static DateTime UpdatePaymentDate(DateTime date, Periodicity periodicity)
    {
        return periodicity switch
        {
            Periodicity.Daily => date.AddDays(1),
            Periodicity.Weekly => date.AddDays(7),
            Periodicity.Monthly => date.AddMonths(1),
            Periodicity.Quarterly => date.AddMonths(3),
            Periodicity.Semiannually => date.AddMonths(6),
            Periodicity.Annually => date.AddYears(1),
            _ => throw new InvalidDataException()
        };
    }
}