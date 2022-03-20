using System.Threading.Tasks;
using WpfUI.ViewModels;

namespace WpfUI.Commands;

public class NewNotebookCommand : CommandBaseAsync
{
    private readonly NotesViewModel _notesViewModel;

    public NewNotebookCommand(NotesViewModel notesViewModel)
    {
        _notesViewModel = notesViewModel;
    }

    public override async Task ExecuteAsync(object parameter)
    {
        await _notesViewModel.CreateNotebook();
    }
}
