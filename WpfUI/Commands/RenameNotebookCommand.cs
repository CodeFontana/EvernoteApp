using WpfUI.ViewModels;

namespace WpfUI.Commands;
public class RenameNotebookCommand : CommandBase
{
    private readonly NotesViewModel _notesViewModel;

    public RenameNotebookCommand(NotesViewModel notesViewModel)
    {
        _notesViewModel = notesViewModel;
    }

    public override void Execute(object parameter)
    {
        _notesViewModel.StartEditing();
    }
}
