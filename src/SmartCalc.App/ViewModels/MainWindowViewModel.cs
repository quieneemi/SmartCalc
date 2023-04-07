using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Reactive;
using System.Reactive.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using ReactiveUI;
using SmartCalc.Core.Interfaces;
using SmartCalc.Core.Models;
using Splat;

namespace SmartCalc.App.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public ObservableCollection<CalcOperation> History { get; }
    
    public string DecimalSeparator { get; }
    public Interaction<string, string> ShowInputDialogInteraction { get; }
    public Interaction<string, Unit> ShowPlotInteraction { get; }
    public Interaction<Unit, Unit> ShowLoanCalcInteraction { get; }
    public Interaction<Unit, Unit> ShowDepositCalcInteraction { get; }
    
    public MainWindowViewModel()
    {
        // configure calc service
        _calcService = Locator.Current.GetService<ISmartCalcService>();
        
        // configure history service
        var historyService = Locator.Current.GetService<IHistoryService>()!;
        History = new ObservableCollection<CalcOperation>(historyService.Read());
        this.WhenAnyValue(x => x.History.Count)
            .Subscribe(_ => historyService.WriteAsync(History));

        DecimalSeparator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
        ShowInputDialogInteraction = new Interaction<string, string>();
        ShowPlotInteraction = new Interaction<string, Unit>();
        ShowLoanCalcInteraction = new Interaction<Unit, Unit>();
        ShowDepositCalcInteraction = new Interaction<Unit, Unit>();
    }
    
    public void TriggerPaneState() => IsPaneOpen = !IsPaneOpen;
    
    public async Task ShowPlot() => await ShowPlotInteraction.Handle(Expression);
    public async Task ShowLoanCalc() => await ShowLoanCalcInteraction.Handle(Unit.Default);
    public async Task ShowDepositCalc() => await ShowDepositCalcInteraction.Handle(Unit.Default);
    
    public void ClearExpression() => Expression = string.Empty;

    public void AddSyllable(string syllable)
    {
        if (Expression.Length + syllable.Length <= 255)
            Expression += syllable;
    }
    
    public void SubtractSyllable()
    {
        if (Expression.Length == 0) return;
        
        var amount = 1;

        if (Expression.EndsWith("ln"))
            amount = 2;
        
        if (Regex.IsMatch(Expression, @"(sin|cos|tan|log|mod)$"))
            amount = 3;
        
        if (Regex.IsMatch(Expression, @"(sqrt|acos|asin|atan)$"))
            amount = 4;
        
        Expression = Expression.Remove(Expression.Length - amount);
    }

    public async Task Calculate()
    {
        if (_calcService is null || Expression.Length == 0) return;
        
        var xValue = 1D;
        if (Expression.Contains('x'))
        {
            var dialogResult = await ShowInputDialogInteraction.Handle("Enter value for X");
            if (double.TryParse(dialogResult, out xValue) is false)
                return;
        }
        
        var result = _calcService.Evaluate(Expression, xValue);
        if (double.IsNaN(result))
        {
            ClearExpression();
            return;
        }
        
        History.Add(new CalcOperation
        {
            Expression = Expression,
            Result = result.ToString("G")
        });
        
        Expression = result.ToString("G");
    }
    
    partial void OnSelectedHistoryItemChanged(CalcOperation? value)
    {
        if (value is null) return;
                
        Expression = value.Value.Result;
        TriggerPaneState();
        SelectedHistoryItem = null;
    }
    
    [ObservableProperty] private string _expression = string.Empty;
    [ObservableProperty] private bool _isPaneOpen;
    [ObservableProperty] private CalcOperation? _selectedHistoryItem;
    
    private readonly ISmartCalcService? _calcService;
}