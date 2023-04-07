using System;
using Avalonia;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;
using SmartCalc.App.ViewModels;

namespace SmartCalc.App.Views;

public partial class InputDialog : ReactiveWindow<InputDialogViewModel>
{
    public InputDialog()
    {
        InitializeComponent();
#if DEBUG
        this.AttachDevTools();
#endif
        
        this.WhenActivated(disposable =>
        {
            disposable(ViewModel!.ReturnValueCommand.Subscribe(Close));
        });
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}