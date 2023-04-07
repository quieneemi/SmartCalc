namespace DepositCalc.Core.Models;

public struct PeriodicalPayment
{
    public Periodicity Periodicity { get; set; }
    public decimal Amount { get; set; }
}