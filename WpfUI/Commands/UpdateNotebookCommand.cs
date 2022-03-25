using System.Threading.Tasks;
using WpfUI.ViewModels;

namespace WpfUI.Commands;

public class UpdateNotebookCommand : CommandBaseAsync
{
    private readonly NotesViewModel _notesViewModel;

    public UpdateNotebookCommand(NotesViewModel notesViewModel)
    {
        _notesViewModel = notesViewModel;
    }

    public override async Task ExecuteAsync(object parameter)
    {
        await _notesViewModel.StopEditingAsync(_notesViewModel.SelectedNotebook);
    }
}
