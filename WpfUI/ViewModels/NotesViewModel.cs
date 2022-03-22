using DataAccessLibrary.Entities;
using DataAccessLibrary.Notes;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using WpfUI.Commands;

namespace WpfUI.ViewModels;

public class NotesViewModel : ViewModelBase
{
    private readonly IServiceProvider _serviceProvider;

    public NotesViewModel()
    {
        // Parameterless default constructor -- for design time
        NewNotebookCommand = new NewNotebookCommand(this);
        NewNoteCommand = new NewNoteCommand(this);
        ExitApplicationCommand = new ExitApplicationCommand();
        NoteTextChangedCommand = new NoteTextChangedCommand(this);
        BoldTextCommand = new BoldTextCommand(this);
        ItalicTextCommand = new ItalicTextCommand(this);
        UnderlineTextCommand = new UnderlineTextCommand(this);
        FontFamilyChangedCommand = new FontFamilyChangedCommand(this);
        FontSizeChangedCommand = new FontSizeChangedCommand(this);
        RenameNotebookCommand = new RenameNotebookCommand(this);
        AvailableFonts = Fonts.SystemFontFamilies.OrderBy(f => f.Source).ToList();
        SelectedFont = AvailableFonts.FirstOrDefault(x => x.Source == "Segoe UI");
        AvailableFontSizes = Enumerable.Range(6, 72).ToList().ConvertAll(i => (double)i);
        SelectedFontSize = 14;
        RenameNotebookTextboxVisibility = Visibility.Collapsed;
        Notebooks = new();
        Notes = new();
    }

    public NotesViewModel(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        NewNotebookCommand = new NewNotebookCommand(this);
        NewNoteCommand = new NewNoteCommand(this);
        ExitApplicationCommand = new ExitApplicationCommand();
        NoteTextChangedCommand = new NoteTextChangedCommand(this);
        BoldTextCommand = new BoldTextCommand(this);
        ItalicTextCommand = new ItalicTextCommand(this);
        UnderlineTextCommand = new UnderlineTextCommand(this);
        FontFamilyChangedCommand = new FontFamilyChangedCommand(this);
        FontSizeChangedCommand = new FontSizeChangedCommand(this);
        RenameNotebookCommand = new RenameNotebookCommand(this);
        AvailableFonts = Fonts.SystemFontFamilies.OrderBy(f => f.Source).ToList();
        SelectedFont = AvailableFonts.FirstOrDefault(x => x.Source == "Segoe UI");
        AvailableFontSizes = Enumerable.Range(6, 72).ToList().ConvertAll(i => (double)i);
        SelectedFontSize = 14;
        RenameNotebookTextboxVisibility = Visibility.Collapsed;
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

    public List<double> AvailableFontSizes { get; set; }

    private double _selectedFontSize;
    public double SelectedFontSize
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

    private Visibility _renameNotebookTextboxVisbility;
    public Visibility RenameNotebookTextboxVisibility
    {
        get
        {
            return _renameNotebookTextboxVisbility;
        }
        set
        {
            _renameNotebookTextboxVisbility = value;
            OnPropertyChanged(nameof(RenameNotebookTextboxVisibility));
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

    public ICommand RenameNotebookCommand { get; set; }

    public async Task CreateNotebook()
    {
        Notebook notebook = new()
        {
            Name = "New notebook"
        };

        using IServiceScope scope = _serviceProvider.CreateScope();
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

        using IServiceScope scope = _serviceProvider.CreateScope();
        NotesDbContext db = scope.ServiceProvider.GetRequiredService<NotesDbContext>();
        await db.Notes.AddAsync(newNote);
        await db.SaveChangesAsync();
        GetNotes();
    }

    private void GetNotebooks()
    {
        using IServiceScope scope = _serviceProvider.CreateScope();
        NotesDbContext db = scope.ServiceProvider.GetRequiredService<NotesDbContext>();
        Notebooks.Clear();
        db.Notebooks.ToList().ForEach(x => Notebooks.Add(x));
    }

    private void GetNotes()
    {
        if (SelectedNotebook != null)
        {
            using IServiceScope scope = _serviceProvider.CreateScope();
            NotesDbContext db = scope.ServiceProvider.GetRequiredService<NotesDbContext>();
            IQueryable<Note> notes = db.Notes.Where(x => x.NotebookId == SelectedNotebook.Id);
            Notes.Clear();
            notes.ToList().ForEach(x => Notes.Add(x));
        }
    }

    public void StartEditing()
    {
        RenameNotebookTextboxVisibility = Visibility.Visible;
    }

    public void StopEditing()
    {
        RenameNotebookTextboxVisibility = Visibility.Collapsed;
    }
}
