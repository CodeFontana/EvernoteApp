using DataAccessLibrary.Entities;
using System.Threading.Tasks;
using WpfUI.ViewModels;

namespace WpfUI.Commands;

public class NewNoteCommandAsync : CommandBaseAsync
{
    private readonly NotesViewModel _notesViewModel;

    public NewNoteCommandAsync(NotesViewModel notesViewModel)
    {
        _notesViewModel = notesViewModel;
    }

    public override async Task ExecuteAsync(object parameter)
    {
        Notebook selectedNotebook = parameter as Notebook;

        if (selectedNotebook != null)
        {
            await _notesViewModel.CreateNoteAsync(selectedNotebook.Id);
        }
    }
}
