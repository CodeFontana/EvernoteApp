using DataAccessLibrary.Entities;
using DataAccessLibrary.Notes;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using WpfUI.Commands;
using WpfUI.Stores;

namespace WpfUI.ViewModels;

public class MainViewModel : ViewModelBase
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly NavigationStore _navigationStore;
    private Notebook _selectedNotebook;

    public MainViewModel(IServiceScopeFactory scopeFactory, NavigationStore navigationStore)
    {
        _scopeFactory = scopeFactory;
        _navigationStore = navigationStore;
        _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
        NewNotebookCommand = new(this);
        NewNoteCommand = new(this);
    }

    public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;

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

    private void OnCurrentViewModelChanged()
    {
        OnPropertyChanged(nameof(CurrentViewModel));
    }
}
