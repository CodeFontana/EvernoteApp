using DataAccessLibrary.Notes;
using System;
using WpfUI.State;
using WpfUI.ViewModels.Factory;
using static WpfUI.ViewModels.Factory.ViewModelFactory;

namespace WpfUI.ViewModels;

public class MainViewModel : ViewModelBase
{
    private readonly ViewModelFactory _viewModelFactory;

    public MainViewModel()
    {
        
    }

    public MainViewModel(Navigator navigator, ViewModelFactory viewModelFactory)
    {
        Navigator = navigator;
        _viewModelFactory = viewModelFactory;
        navigator.CurrentViewModel = _viewModelFactory.CreateViewModel(ViewType.Notes);
    }

    public Navigator Navigator { get; }
}
