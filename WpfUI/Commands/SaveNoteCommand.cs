using System.Threading.Tasks;
using WpfUI.ViewModels;

namespace WpfUI.Commands;

public class SaveNoteCommand : CommandBaseAsync
{
    private readonly NotesViewModel _notesViewModel;

    public SaveNoteCommand(NotesViewModel notesViewModel)
    {
        _notesViewModel = notesViewModel;
    }

    public override async Task ExecuteAsync(object parameter)
    {
        await _notesViewModel.SaveNote();
    }
}
