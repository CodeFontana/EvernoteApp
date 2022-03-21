using DataAccessLibrary.Entities;
using DataAccessLibrary.Notes;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using WpfUI.Commands;

namespace WpfUI.ViewModels;

public class NotesViewModel : ViewModelBase
{
    private readonly IServiceScopeFactory _scopeFactory;

    public NotesViewModel()
    {
        // Parameterless default constructor -- for design time
    }

    public NotesViewModel(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
        NewNotebookCommand = new NewNotebookCommand(this);
        NewNoteCommand = new NewNoteCommand(this);
        ExitApplicationCommand = new ExitApplicationCommand();
        NoteTextChangedCommand = new NoteTextChangedCommand(this);
        BoldTextCommand = new BoldTextCommand(this);
        ItalicTextCommand = new ItalicTextCommand(this);
        UnderlineTextCommand = new UnderlineTextCommand(this);
        FontFamilyChangedCommand = new FontFamilyChangedCommand(this);
        FontSizeChangedCommand = new FontSizeChangedCommand(this);
        AvailableFonts = Fonts.SystemFontFamilies.OrderBy(f => f.Source).ToList();
        SelectedFont = AvailableFonts.FirstOrDefault(x => x.Source == "Calibri");
        AvailableFontSizes = Enumerable.Range(6, 72).ToList();
        SelectedFontSize = 12;
        Notebooks = new();
        Notes = new();
        GetNotebooks();
    }

    public ObservableCollection<Notebook> Notebooks { get; set; }

    private Notebook _selectedNotebook;
    public Notebook SelectedNotebook
    {
        get { return _selectedNotebook; }
        set 
        { 
            _selectedNotebook = value;
            OnPropertyChanged(nameof(SelectedNotebook));
            GetNotes();
        }
    }

    public ObservableCollection<Note> Notes { get; set; }

    private string _currentNoteXaml;
    public string CurrentNoteXaml
    {
        get { return _currentNoteXaml; }
        set 
        { 
            _currentNoteXaml = value;
            OnPropertyChanged(nameof(CurrentNoteXaml));
        }
    }

    private int _currentNoteCharCount;
    public int CurrentNoteCharCount
    {
        get { return _currentNoteCharCount; }
        set 
        { 
            _currentNoteCharCount = value;
            OnPropertyChanged(nameof(CurrentNoteCharCount));
            OnPropertyChanged(nameof(CurrentNoteCharCountStatusMessage));
        }
    }

    public string CurrentNoteCharCountStatusMessage
    {
        get { return $"Document length: {CurrentNoteCharCount} characters"; }
    }

    public ICollection<FontFamily> AvailableFonts { get; }

    private FontFamily _selectedFont;
    public FontFamily SelectedFont
    {
        get { return _selectedFont; }
        set 
        { 
            _selectedFont = value;
            OnPropertyChanged(nameof(SelectedFont));
        }
    }

    public List<int> AvailableFontSizes { get; set; }

    private int _selectedFontSize;
    public int SelectedFontSize
    {
        get
        {
            return _selectedFontSize;
        }
        set
        {
            _selectedFontSize = value;
            OnPropertyChanged(nameof(SelectedFontSize));
        }
    }

    public ICommand NewNotebookCommand { get; set; }

    public ICommand NewNoteCommand { get; set; }

    public ICommand ExitApplicationCommand { get; set; }

    public ICommand NoteTextChangedCommand { get; set; }

    public ICommand BoldTextCommand { get; set; }
    public ICommand ItalicTextCommand { get; set; }
    public ICommand UnderlineTextCommand { get; set; }
    public ICommand FontFamilyChangedCommand { get; set; }
    public ICommand FontSizeChangedCommand { get; set; }

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
        GetNotebooks();
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
        GetNotes();
    }

    private void GetNotebooks()
    {
        using IServiceScope scope = _scopeFactory.CreateScope();
        NotesDbContext db = scope.ServiceProvider.GetRequiredService<NotesDbContext>();
        Notebooks.Clear();
        db.Notebooks.ToList().ForEach(x => Notebooks.Add(x));
    }

    private void GetNotes()
    {
        if (SelectedNotebook != null)
        {
            using IServiceScope scope = _scopeFactory.CreateScope();
            NotesDbContext db = scope.ServiceProvider.GetRequiredService<NotesDbContext>();
            IQueryable<Note> notes = db.Notes.Where(x => x.NotebookId == SelectedNotebook.Id);
            Notes.Clear();
            notes.ToList().ForEach(x => Notes.Add(x));
        }
    }
}
