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
        if (parameter is int notebookId)
        {
            _notesViewModel.StartEditing(notebookId);
        }
    }
}
