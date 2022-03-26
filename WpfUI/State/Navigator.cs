using System.Windows.Input;
using WpfUI.Commands;
using WpfUI.ViewModels;
using WpfUI.ViewModels.Factory;

namespace WpfUI.State;

public class Navigator
{
    private readonly ViewModelFactory _viewModelFactory;

    public Navigator(ViewModelFactory viewModelFactory)
    {
        _viewModelFactory = viewModelFactory;
        NavigateCommand = new NavigateCommand(this, _viewModelFactory);
    }

    public ViewModelBase CurrentViewModel { get; set; }

    public ICommand NavigateCommand { get; set; }
}
