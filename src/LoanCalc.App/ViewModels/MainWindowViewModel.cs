using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using LoanCalc.Core.Interfaces;
using LoanCalc.Core.Models;
using ReactiveUI;
using Splat;

namespace LoanCalc.App.ViewModels;

public class MainWindowViewModel
{
    public Interaction<ResultData, Unit> ShowResultWindowInteraction { get; }
    
    public MainWindowViewModel()
    {
        ShowResultWindowInteraction = new Interaction<ResultData, Unit>();
        
        _calcService = Locator.Current.GetService<ILoanCalcService>();
    }

    public async Task Calculate(ReadOnlyCollection<object> parameters)
    {
        if (_calcService is null) return;

        var data = new SourceData
        {
            Amount = (decimal)parameters[0],
            InterestRate = (decimal)parameters[1],
            Term = (int)(decimal)parameters[2],
            Type = (Type)parameters[3]
        };

        var result = _calcService.Calculate(data);

        await ShowResultWindowInteraction.Handle(result);
    }

    private readonly ILoanCalcService? _calcService;
}