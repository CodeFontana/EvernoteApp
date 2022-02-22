using DataAccessLibrary.Entities;
using System;
using System.Collections.ObjectModel;
using WpfUI.ViewModels.Commands;

namespace WpfUI.ViewModels
{
    public class NotesViewModel
    {
        public NotesViewModel()
        {
            NewNotebookCommand = new(this);
            NewNoteCommand = new(this);
        }

        public ObservableCollection<Notebook> Notebooks { get; set; }

        private Notebook _selectedNotebook;

        public Notebook SelectedNotebook
        {
            get { return _selectedNotebook; }
            set { _selectedNotebook = value; }
        }

        public ObservableCollection<Note> Notes { get; set; }

        public NewNotebookCommand NewNotebookCommand { get; set; }

        public NewNoteCommand NewNoteCommand { get; set; }

        public void CreateNote(int notebookId)
        {
            Note newNote = new()
            {
                NotebookId = notebookId,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Title = "New note"
            };
        }
    }
}
