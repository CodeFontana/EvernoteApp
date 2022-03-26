using System.Windows.Input;
using WpfUI.Commands;
using WpfUI.State;
using WpfUI.ViewModels.Factory;
using static WpfUI.ViewModels.Factory.ViewModelFactory;

namespace WpfUI.ViewModels;

public class MainViewModel : ViewModelBase
{
    public MainViewModel(Navigator navigator, ViewModelFactory viewModelFactory)
    {
        Navigator = navigator;
        NavigateCommand = new NavigateCommand(navigator, viewModelFactory);
        NavigateCommand.Execute(ViewType.Notes);
    }

    public Navigator Navigator { get; }

    public ICommand NavigateCommand { get; set; }
}
