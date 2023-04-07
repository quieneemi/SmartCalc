using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace LoanCalc.App.Views;

public partial class ResultWindow : Window
{
    public ResultWindow()
    {
        InitializeComponent();
#if DEBUG
        this.AttachDevTools();
#endif
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}