using System.Reactive;
using CommunityToolkit.Mvvm.ComponentModel;
using ReactiveUI;

namespace SmartCalc.App.ViewModels;

public partial class InputDialogViewModel : ViewModelBase
{
    public string Title { get; }

    public ReactiveCommand<Unit, string> ReturnValueCommand { get; }
    
    public InputDialogViewModel(string title)
    {
        Title = title;
        ReturnValueCommand = ReactiveCommand.Create(() => Text);
    }
    
    [ObservableProperty] private string _text = string.Empty;
}