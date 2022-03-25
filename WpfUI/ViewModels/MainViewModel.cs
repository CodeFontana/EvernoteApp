using DataAccessLibrary.Notes;
using System;

namespace WpfUI.ViewModels;

public class MainViewModel : ViewModelBase
{
    public MainViewModel()
    {
        CurrentViewModel = CreateViewModel(ViewType.Notes);
    }

    public enum ViewType
    {
        Notes,
        Login
    }

    private ViewModelBase _currentViewModel;
    public ViewModelBase CurrentViewModel
    {
        get => _currentViewModel;
        set
        {
            _currentViewModel = value;
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }

    public ViewModelBase CreateViewModel(ViewType viewType)
    {
        switch (viewType)
        {
            case ViewType.Notes:
                return new NotesViewModel();
            case ViewType.Login:
                return new LoginViewModel();
            default:
                throw new ArgumentException("Invalid view type");
        }
    }
}
