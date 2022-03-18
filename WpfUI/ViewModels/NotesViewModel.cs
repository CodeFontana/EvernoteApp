using DataAccessLibrary.Entities;
using DataAccessLibrary.Notes;
using System;
using System.Collections.ObjectModel;
using WpfUI.Commands;

namespace WpfUI.ViewModels
{
    public class NotesViewModel
    {
        private readonly NotesDbContext _dbContext;
        private Notebook _selectedNotebook;

        public NotesViewModel(NotesDbContext dbContext)
        {
            NewNotebookCommand = new(this);
            NewNoteCommand = new(this);
            _dbContext = dbContext;
        }

        public ObservableCollection<Notebook> Notebooks { get; set; }

        public Notebook SelectedNotebook
        {
            get { return _selectedNotebook; }
            set { _selectedNotebook = value; }
        }

        public ObservableCollection<Note> Notes { get; set; }

        public NewNotebookCommand NewNotebookCommand { get; set; }

        public NewNoteCommand NewNoteCommand { get; set; }

        public void CreateNotebook()
        {
            Notebook notebook = new()
            {
                Name = "New notebook"
            };

            _dbContext.Notebooks.Add(notebook);
        }

        public void CreateNote(int notebookId)
        {
            Note newNote = new()
            {
                NotebookId = notebookId,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Title = "New note"
            };

            _dbContext.Notes.Add(newNote);
        }
    }
}
