using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using DepositCalc.Core;
using DepositCalc.Core.Interfaces;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LoanCalc.Core;
using LoanCalc.Core.Interfaces;
using SmartCalc.App.ViewModels;
using SmartCalc.App.Views;
using SmartCalc.Core;
using SmartCalc.Core.Interfaces;
using Splat;

namespace SmartCalc.App;

public class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
        
        Locator.CurrentMutable.RegisterConstant(new HistoryService(), typeof(IHistoryService));
        Locator.CurrentMutable.RegisterConstant(new SmartCalcService(), typeof(ISmartCalcService));
        Locator.CurrentMutable.RegisterLazySingleton(() => new LoanCalcService(), typeof(ILoanCalcService));
        Locator.CurrentMutable.RegisterLazySingleton(() => new DepositCalcService(), typeof(IDepositCalcService));
        
        LiveCharts.Configure(config => 
            config 
                .AddSkiaSharp()
                .AddDefaultMappers()
                .AddLightTheme()
        );
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = new MainWindowViewModel(),
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}