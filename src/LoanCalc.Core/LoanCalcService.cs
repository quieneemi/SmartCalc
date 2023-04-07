using LoanCalc.Core.Interfaces;
using LoanCalc.Core.Models;
using Type = LoanCalc.Core.Models.Type;

namespace LoanCalc.Core;

public class LoanCalcService : ILoanCalcService
{
    public ResultData Calculate(SourceData data)
    {
        return data.Type == Type.Annuity
            ? CalcAnnuity(data)
            : CalcDifferentiated(data);
    }

    private static ResultData CalcAnnuity(SourceData data)
    {
        var rate = data.InterestRate / 12;
        var temp = (decimal)(1 - Math.Pow((double)(1 + rate), data.Term * -1));
        var monthlyPayment = data.Amount * rate / temp;
        var totalPayment = monthlyPayment * data.Term;
        var overpayment = totalPayment - data.Amount;
        var debt = data.Amount;
        var payments = new List<Payment>();
        
        for (var i = 1; i <= data.Term; i++)
        {
            var interests = debt * rate;
            debt -= monthlyPayment - interests;
            
            payments.Add(new Payment
            {
                Id = i,
                Amount = monthlyPayment,
                PrincipalPayment = monthlyPayment - interests,
                InterestsPayment = interests,
                DebtAmount = debt
            });
        }

        return new ResultData
        {
            MonthlyPayments = payments.ToArray(),
            TotalRepayment = totalPayment,
            Overpayment = overpayment
        };
    }
    
    private static ResultData CalcDifferentiated(SourceData data)
    {
        var rate = data.InterestRate / 12;
        var debt = data.Amount;
        var monthlyPayment = debt / data.Term;
        var totalPayment = 0m;
        var payments = new List<Payment>();

        for (var i = data.Term; i > 0; i--)
        {
            var interests = debt * rate;
            var payment = monthlyPayment + interests;
            totalPayment += payment;
            debt -= monthlyPayment;
            payments.Add(new Payment
            {
                Id = data.Term - i + 1,
                Amount = payment,
                PrincipalPayment = monthlyPayment,
                InterestsPayment = interests,
                DebtAmount = debt
            });
        }

        return new ResultData
        {
            MonthlyPayments = payments.ToArray(),
            Overpayment = totalPayment - data.Amount,
            TotalRepayment = totalPayment
        };
    }
}