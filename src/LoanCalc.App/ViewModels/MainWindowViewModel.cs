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

    public async Task Calculate(object data)
    {
        if (_calcService is null ||
            data is not SourceData sourceData) return;

        var result = _calcService.Calculate(sourceData);

        await ShowResultWindowInteraction.Handle(result);
    }

    private readonly ILoanCalcService? _calcService;
}