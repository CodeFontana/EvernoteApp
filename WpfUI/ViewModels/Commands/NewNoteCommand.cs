using DataAccessLibrary.Entities;
using System;
using System.Windows.Input;

namespace WpfUI.ViewModels.Commands
{
    public class NewNoteCommand : ICommand
    {
        private readonly NotesViewModel _notesViewModel;

        public event EventHandler CanExecuteChanged;

        public NewNoteCommand(NotesViewModel notesViewModel)
        {
            _notesViewModel = notesViewModel;
        }

        public bool CanExecute(object parameter)
        {
            Notebook selectedNotebook = parameter as Notebook;

            if (selectedNotebook != null)
            {
                return true;
            }

            return false;
        }

        public void Execute(object parameter)
        {
            Notebook selectedNotebook = parameter as Notebook;

        }
    }
}
