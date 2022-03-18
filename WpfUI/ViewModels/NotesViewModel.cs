using DataAccessLibrary.Entities;
using DataAccessLibrary.Notes;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
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

        public async Task CreateNotebook()
        {
            Notebook notebook = new()
            {
                Name = "New notebook"
            };

            await _dbContext.Notebooks.AddAsync(notebook);
            await _dbContext.SaveChangesAsync();
        }

        public async Task CreateNote(int notebookId)
        {
            Note newNote = new()
            {
                NotebookId = notebookId,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Title = "New note"
            };

            await _dbContext.Notes.AddAsync(newNote);
            await _dbContext.SaveChangesAsync();
        }
    }
}
