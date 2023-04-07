namespace DepositCalc.Core.Models;

public struct ResultData
{
    public decimal Interests { get; set; }
    public decimal Taxes { get; set; }
    public decimal Amount { get; set; }
}