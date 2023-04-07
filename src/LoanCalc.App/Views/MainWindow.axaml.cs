using System.Reactive;
using System.Threading.Tasks;
using Avalonia.ReactiveUI;
using LoanCalc.App.ViewModels;
using LoanCalc.Core.Models;
using ReactiveUI;

namespace LoanCalc.App.Views;

public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
{
    public MainWindow()
    {
        InitializeComponent();
        
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