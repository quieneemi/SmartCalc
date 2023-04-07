using System;
using System.Reactive;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.ReactiveUI;
using DepositCalc.App.ViewModels;
using DepositCalc.Core.Models;
using ReactiveUI;

namespace DepositCalc.App.Views;

public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
{
    public MainWindow()
    {
        InitializeComponent();
        
        this.FindControl<DatePicker>("StartDate")!.SelectedDate = DateTime.Today;
        this.FindControl<DatePicker>("WithdrawalDate")!.SelectedDate = DateTime.Today.AddMonths(1);
        this.FindControl<DatePicker>("AdditionDate")!.SelectedDate = DateTime.Today.AddMonths(1);
        
        this.WhenActivated(disposable =>
        {
            disposable(ViewModel!.ShowResultWindowInteraction.RegisterHandler(DoShowResultWindow));
        });
    }
    
    private async Task DoShowResultWindow(InteractionContext<ResultData, Unit> context)
    {
        var window = new ResultWindow
        {
            DataContext = new ResultWindowViewModel(context.Input)
        };

        await window.ShowDialog<Unit>(this);
    }
}