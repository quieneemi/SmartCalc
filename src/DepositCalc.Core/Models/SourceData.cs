namespace DepositCalc.Core.Models;

public struct SourceData
{
    public decimal Amount { get; set; }
    public int Term { get; set; }
    public DateTime StartDate { get; set; }
    public decimal InterestRate { get; set; }
    public decimal TaxRate { get; set; }
    public bool Capitalization { get; set; }
    public Periodicity CapitalizationPeriodicity { get; set; }
    public PeriodicalPayment PeriodicalAdditions { get; set; }
    public PeriodicalPayment PeriodicalWithdrawals { get; set; }
    public List<Payment> Additions { get; set; }
    public List<Payment> Withdrawals { get; set; }
}