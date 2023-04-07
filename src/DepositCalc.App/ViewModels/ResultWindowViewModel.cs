using DepositCalc.Core.Models;

namespace DepositCalc.App.ViewModels;

public class ResultWindowViewModel
{
    public string Interests { get; }
    public string Taxes { get; }
    public string Amount { get; }
    
    public ResultWindowViewModel(ResultData data)
    {
        Interests = data.Interests.ToString("C");
        Taxes = data.Taxes.ToString("C");
        Amount = data.Amount.ToString("C");
    }
}