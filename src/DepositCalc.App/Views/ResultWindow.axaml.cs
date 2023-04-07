using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace DepositCalc.App.Views;

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