using WpfUI.ViewModels;

namespace WpfUI.Commands;

public class RenameNoteCommand : CommandBase
{
    private readonly NotesViewModel _notesViewModel;

    public RenameNoteCommand(NotesViewModel notesViewModel)
    {
        _notesViewModel = notesViewModel;
    }

    public override void Execute(object parameter)
    {
        _notesViewModel.StartEditingNote();
    }
}
