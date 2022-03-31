using DataAccessLibrary.Notes;
using WpfUI.ViewModels;

namespace WpfUI.Commands;

public class NavigateCommand : CommandBase
{
    private readonly MainViewModel _mainViewModel;
    private readonly NotesRepositoryFactory _notesRepositoryFactory;

    public enum ViewType
    {
        Login,
        Notes
    }


    public NavigateCommand(MainViewModel mainViewModel, NotesRepositoryFactory notesRepositoryFactory)
    {
        _mainViewModel = mainViewModel;
        _notesRepositoryFactory = notesRepositoryFactory;
    }

    public override void Execute(object parameter)
    {
        if (parameter is ViewType viewType)
        {
            switch (viewType)
            {
                case ViewType.Notes:
                    _mainViewModel.CurrentVM = new NotesViewModel(_notesRepositoryFactory);
                    break;
                default:
                    break;
            }
        }
    }
}
