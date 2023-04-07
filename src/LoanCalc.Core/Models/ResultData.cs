namespace LoanCalc.Core.Models;

public struct ResultData
{
    public decimal Overpayment { get; set; }
    public decimal TotalRepayment { get; set; }
    public Payment[] MonthlyPayments { get; set; }
}