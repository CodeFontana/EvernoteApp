using System.Threading.Tasks;
using WpfUI.ViewModels;

namespace WpfUI.Commands;

public class NewNotebookCommandAsync : CommandBaseAsync
{
    private readonly NotesViewModel _notesViewModel;

    public NewNotebookCommandAsync(NotesViewModel notesViewModel)
    {
        _notesViewModel = notesViewModel;
    }

    public override async Task ExecuteAsync(object parameter)
    {
        await _notesViewModel.CreateNotebookAsync();
    }
}
