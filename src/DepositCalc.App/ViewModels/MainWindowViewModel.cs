using System;
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
    
    public void AddAddition(ReadOnlyCollection<object> parameters)
    {
        var addition = new Payment
        {
            Date = ((DateTimeOffset)parameters[0]).Date,
            Amount = (decimal)parameters[1]
        };
        Additions.Add(addition);
    }

    public void RemoveAddition(Payment addition) => Additions.Remove(addition);

    public void AddWithdrawal(ReadOnlyCollection<object> parameters)
    {
        var withdrawal = new Payment
        {
            Date = ((DateTimeOffset)parameters[0]).Date,
            Amount = (decimal)parameters[1]
        };
        Withdrawals.Add(withdrawal);
    }
    
    public void RemoveWithdrawal(Payment withdrawal) => Withdrawals.Remove(withdrawal);

    public async Task Calculate(ReadOnlyCollection<object> parameters)
    {
        if (_calcService is null) return;
        
        var data = new SourceData
        {
            Amount = (decimal)parameters[0],
            Term = (int)(decimal)parameters[1],
            StartDate = ((DateTimeOffset)parameters[2]).Date,
            InterestRate = (decimal)parameters[3],
            TaxRate = (decimal)parameters[4],
            Capitalization = (bool)parameters[5],
            CapitalizationPeriodicity = (Periodicity)parameters[6],
            PeriodicalAdditions = new PeriodicalPayment
            {
                Periodicity = (Periodicity)parameters[7],
                Amount = (decimal)parameters[8]
            },
            PeriodicalWithdrawals = new PeriodicalPayment
            {
                Periodicity = (Periodicity)parameters[9],
                Amount = (decimal)parameters[10]
            },
            Additions = Additions.ToList(),
            Withdrawals = Withdrawals.ToList()
        };

        var result = _calcService.Calculate(data);

        await ShowResultWindowInteraction.Handle(result);
    }

    private readonly IDepositCalcService? _calcService;
}