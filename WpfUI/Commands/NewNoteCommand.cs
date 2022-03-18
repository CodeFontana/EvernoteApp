using DataAccessLibrary.Entities;
using System;
using System.Windows.Input;
using WpfUI.ViewModels;

namespace WpfUI.Commands;

public class NewNoteCommand : CommandBase
{
    private readonly NotesViewModel _notesViewModel;

    public NewNoteCommand(NotesViewModel notesViewModel)
    {
        _notesViewModel = notesViewModel;
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

    public override void Execute(object parameter)
    {
        Notebook selectedNotebook = parameter as Notebook;
        _notesViewModel.CreateNote(selectedNotebook.Id);
    }
}
