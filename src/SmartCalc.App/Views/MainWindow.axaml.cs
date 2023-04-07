using System.Reactive;
using System.Threading.Tasks;
using Avalonia.ReactiveUI;
using ReactiveUI;
using SmartCalc.App.ViewModels;

using PlotWindow = SmartCalc.Plot.Views.MainWindow;
using PlotWindowViewModel = SmartCalc.Plot.ViewModels.MainWindowViewModel;

using LoanCalcWindow = LoanCalc.App.Views.MainWindow;
using LoanCalcWindowViewModel = LoanCalc.App.ViewModels.MainWindowViewModel;

using DepositCalcWindow = DepositCalc.App.Views.MainWindow;
using DepositCalcWindowViewModel = DepositCalc.App.ViewModels.MainWindowViewModel;

namespace SmartCalc.App.Views;

public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
{
    public MainWindow()
    {
        InitializeComponent();
        
        this.WhenActivated(disposable =>
        {
            disposable(ViewModel!.ShowInputDialogInteraction.RegisterHandler(DoShowInputDialog));
            disposable(ViewModel!.ShowPlotInteraction.RegisterHandler(DoShowPlot));
            disposable(ViewModel!.ShowLoanCalcInteraction.RegisterHandler(DoShowLoanCalc));
            disposable(ViewModel!.ShowDepositCalcInteraction.RegisterHandler(DoShowDepositCalc));
        });
    }
    
    private async Task DoShowInputDialog(InteractionContext<string, string> context)
    {
        var dialog = new InputDialog { DataContext = new InputDialogViewModel(context.Input) };
        var result = await dialog.ShowDialog<string>(this);
        context.SetOutput(result);
    }

    private async Task DoShowPlot(InteractionContext<string, Unit> context)
    {
        var window = new PlotWindow { DataContext = new PlotWindowViewModel(context.Input) };
        await window.ShowDialog<Unit>(this);
    }

    private async Task DoShowLoanCalc(InteractionContext<Unit, Unit> context)
    {
        var window = new LoanCalcWindow { DataContext = new LoanCalcWindowViewModel() };
        await window.ShowDialog<Unit>(this);
    }
    
    private async Task DoShowDepositCalc(InteractionContext<Unit, Unit> context)
    {
        var window = new DepositCalcWindow { DataContext = new DepositCalcWindowViewModel() };
        await window.ShowDialog<Unit>(this);
    }
}