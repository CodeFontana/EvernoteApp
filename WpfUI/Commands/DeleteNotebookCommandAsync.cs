using System.Threading.Tasks;
using WpfUI.ViewModels;

namespace WpfUI.Commands;

public class DeleteNotebookCommandAsync : CommandBaseAsync
{
    private readonly NotesViewModel _notesViewModel;

    public DeleteNotebookCommandAsync(NotesViewModel notesViewModel)
    {
        _notesViewModel = notesViewModel;
    }

    public override async Task ExecuteAsync(object parameter)
    {
        if (parameter is int notebookId)
        {
            await _notesViewModel.DeleteNotebookAsync(notebookId);
        }
    }
}
