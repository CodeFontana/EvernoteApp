using System;
using System.Windows.Input;

namespace WpfUI.ViewModels.Commands
{
    public class NewNotebookCommand : ICommand
    {
        private readonly NotesViewModel _notesViewModel;

        public event EventHandler CanExecuteChanged;

        public NewNotebookCommand(NotesViewModel notesViewModel)
        {
            _notesViewModel = notesViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            throw new NotImplementedException();
        }
    }
}
