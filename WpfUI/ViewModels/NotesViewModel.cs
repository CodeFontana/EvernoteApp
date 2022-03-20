using DataAccessLibrary.Entities;
using DataAccessLibrary.Notes;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfUI.Commands;

namespace WpfUI.ViewModels;

public class NotesViewModel : ViewModelBase
{
    private readonly IServiceScopeFactory _scopeFactory;
    private Notebook _selectedNotebook;

    public NotesViewModel(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
        NewNotebookCommand = new NewNotebookCommand(this);
        NewNoteCommand = new NewNoteCommand(this);
    }

    public ObservableCollection<Notebook> Notebooks { get; set; }

    public Notebook SelectedNotebook
    {
        get { return _selectedNotebook; }
        set { _selectedNotebook = value; }
    }

    public ObservableCollection<Note> Notes { get; set; }

    public ICommand NewNotebookCommand { get; set; }

    public ICommand NewNoteCommand { get; set; }

    public async Task CreateNotebook()
    {
        Notebook notebook = new()
        {
            Name = "New notebook"
        };

        using IServiceScope scope = _scopeFactory.CreateScope();
        NotesDbContext db = scope.ServiceProvider.GetRequiredService<NotesDbContext>();
        await db.Notebooks.AddAsync(notebook);
        await db.SaveChangesAsync();
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

        using IServiceScope scope = _scopeFactory.CreateScope();
        NotesDbContext db = scope.ServiceProvider.GetRequiredService<NotesDbContext>();
        await db.Notes.AddAsync(newNote);
        await db.SaveChangesAsync();
    }
}
