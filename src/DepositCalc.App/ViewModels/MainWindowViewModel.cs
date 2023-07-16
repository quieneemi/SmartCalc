using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using DepositCalc.Core.Interfaces;
using DepositCalc.Core.Models;
using ReactiveUI;
using Splat;

namespace DepositCalc.App.ViewModels;

public class MainWindowViewModel
{
    public ObservableCollection<Payment> Additions { get; }
    public ObservableCollection<Payment> Withdrawals { get; }
    
    public Interaction<ResultData, Unit> ShowResultWindowInteraction { get; }

    public MainWindowViewModel()
    {
        Additions = new ObservableCollection<Payment>();
        Withdrawals = new ObservableCollection<Payment>();

        ShowResultWindowInteraction = new Interaction<ResultData, Unit>();

        _calcService = Locator.Current.GetService<IDepositCalcService>();
    }

    public void AddAddition(object payment)
    {
        if (payment is Payment addition)
            Additions.Add(addition);
    }

    public void RemoveAddition(Payment addition)
        => Additions.Remove(addition);

    public void AddWithdrawal(object payment)
    {
        if (payment is Payment withdrawal)
            Additions.Add(withdrawal);
    }

    public void RemoveWithdrawal(Payment withdrawal)
        => Withdrawals.Remove(withdrawal);

    public async Task Calculate(object? data)
    {
        if (_calcService is null ||
            data is not SourceData sourceData) return;

        sourceData.Additions = Additions.ToList();
        sourceData.Withdrawals = Withdrawals.ToList();
        var result = _calcService.Calculate(sourceData);

        await ShowResultWindowInteraction.Handle(result);
    }

    private readonly IDepositCalcService? _calcService;
}