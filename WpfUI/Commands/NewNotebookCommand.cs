using System;
using System.Windows.Input;
using WpfUI.ViewModels;

namespace WpfUI.Commands;

public class NewNotebookCommand : CommandBase
{
    private readonly NotesViewModel _notesViewModel;

    public NewNotebookCommand(NotesViewModel notesViewModel)
    {
        _notesViewModel = notesViewModel;
    }

    public override void Execute(object parameter)
    {
        _notesViewModel.CreateNotebook();
    }
}
