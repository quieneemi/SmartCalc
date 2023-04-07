namespace LoanCalc.Core.Models;

public struct Payment
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public decimal PrincipalPayment { get; set; }
    public decimal InterestsPayment { get; set; }
    public decimal DebtAmount { get; set; }
}