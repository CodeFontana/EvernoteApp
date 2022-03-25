using WpfUI.State;
using WpfUI.ViewModels;
using WpfUI.ViewModels.Factory;
using static WpfUI.ViewModels.Factory.ViewModelFactory;

namespace WpfUI.Commands;

public class NavigateCommand : CommandBase
{
    private readonly Navigator _navigator;
    private readonly ViewModelFactory _viewModelFactory;

    public NavigateCommand(Navigator navigator, ViewModelFactory viewModelFactory)
    {
        _navigator = navigator;
        _viewModelFactory = viewModelFactory;
    }

    public override void Execute(object parameter)
    {
        if (parameter is ViewType viewType)
        {
            _navigator.CurrentViewModel = _viewModelFactory.CreateViewModel(viewType);
        }
    }
}
