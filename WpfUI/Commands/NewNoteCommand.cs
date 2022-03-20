using DataAccessLibrary.Entities;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfUI.ViewModels;

namespace WpfUI.Commands;

public class NewNoteCommand : CommandBaseAsync
{
    private readonly MainViewModel _mainViewModel;

    public NewNoteCommand(MainViewModel mainViewModel)
    {
        _mainViewModel = mainViewModel;
    }

    public override bool CanExecute(object parameter)
    {
        Notebook selectedNotebook = parameter as Notebook;

        if (selectedNotebook != null)
        {
            return true;
        }

        return false;
    }

    public override async Task ExecuteAsync(object parameter)
    {
        Notebook selectedNotebook = parameter as Notebook;
        await _mainViewModel.CreateNote(selectedNotebook.Id);
    }
}
