using System;

namespace WpfUI.ViewModels.Factory;

public class ViewModelFactory
{
    private readonly NotesViewModelFactory _notesViewModelFactory;
    private readonly LoginViewModelFactory _loginViewModelFactory;

    public enum ViewType
    {
        Notes,
        Login
    }

    public ViewModelFactory(NotesViewModelFactory notesViewModelFactory,
                            LoginViewModelFactory loginViewModelFactory)
    {
        _notesViewModelFactory = notesViewModelFactory;
        _loginViewModelFactory = loginViewModelFactory;
    }

    public ViewModelBase CreateViewModel(ViewType viewType)
    {
        switch (viewType)
        {
            case ViewType.Notes:
                return _notesViewModelFactory.CreateViewModel();
            case ViewType.Login:
                return _loginViewModelFactory.CreateViewModel();
            default:
                throw new ArgumentException("Invalid view type", "viewType");
        }
    }
}
