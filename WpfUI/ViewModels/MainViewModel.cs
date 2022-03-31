using DataAccessLibrary.Notes;
using System.Windows.Input;
using WpfUI.Commands;
using static WpfUI.Commands.NavigateCommand;

namespace WpfUI.ViewModels;

public class MainViewModel : ViewModelBase
{
    public MainViewModel(NotesRepositoryFactory notesRepositoryFactory)
    {
        
        NavigateCommand = new NavigateCommand(this, notesRepositoryFactory);
        NavigateCommand.Execute(ViewType.Notes);
    }

    public ViewModelBase CurrentVM { get; set; }
    public NotesViewModel NotesVM { get; set; }

    public ICommand NavigateCommand { get; set; }
}
