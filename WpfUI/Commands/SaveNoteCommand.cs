using System.Threading.Tasks;
using WpfUI.ViewModels;

namespace WpfUI.Commands;

public class SaveNoteCommand : CommandBase
{
    private readonly NotesViewModel _notesViewModel;

    public SaveNoteCommand(NotesViewModel notesViewModel)
    {
        _notesViewModel = notesViewModel;
    }

    public override void Execute(object parameter)
    {
        _notesViewModel.SaveNote();
    }
}
