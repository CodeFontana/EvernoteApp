using System.Threading.Tasks;
using WpfUI.ViewModels;

namespace WpfUI.Commands;

public class DeleteNotebookCommand : CommandBaseAsync
{
    private readonly NotesViewModel _notesViewModel;

    public DeleteNotebookCommand(NotesViewModel notesViewModel)
    {
        _notesViewModel = notesViewModel;
    }

    public override async Task ExecuteAsync(object parameter)
    {
        if (parameter is int notebookId)
        {
            await _notesViewModel.DeleteNotebook(notebookId);
        }
    }
}
