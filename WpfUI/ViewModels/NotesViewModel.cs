using DataAccessLibrary.Entities;
using DataAccessLibrary.Notes;
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
    public NotesViewModel()
    {
        NewNotebookCommand = new NewNotebookCommandAsync(this);
        NewNoteCommand = new NewNoteCommandAsync(this);
        ExitApplicationCommand = new ExitApplicationCommand();
        NoteTextChangedCommand = new NoteTextChangedCommand(this);
        BoldTextCommand = new BoldTextCommand(this);
        ItalicTextCommand = new ItalicTextCommand(this);
        UnderlineTextCommand = new UnderlineTextCommand(this);
        FontFamilyChangedCommand = new FontFamilyChangedCommand(this);
        FontSizeChangedCommand = new FontSizeChangedCommand(this);
        RenameNotebookCommand = new RenameNotebookCommand(this);
        DeleteNoteCommand = new DeleteNoteCommandAsync(this);
        DeleteNotebookCommand = new DeleteNotebookCommandAsync(this);
        AvailableFonts = Fonts.SystemFontFamilies.OrderBy(f => f.Source).ToList();
        SelectedFont = AvailableFonts.FirstOrDefault(x => x.Source == "Segoe UI");
        AvailableFontSizes = Enumerable.Range(6, 72).ToList().ConvertAll(i => (double)i);
        SelectedFontSize = 14;
        RenameNotebookTextboxVisibility = Visibility.Collapsed;
        Notebooks = new();
        Notes = new();
        _ = GetNotebooksAsync();
    }

    private ObservableCollection<Notebook> _notebooks;
    public ObservableCollection<Notebook> Notebooks
    {
        get => _notebooks;
        set
        {
            _notebooks = value;
            OnPropertyChanged(nameof(Notebooks));
        }
    }

    private Notebook _selectedNotebook;
    public Notebook SelectedNotebook
    {
        get => _selectedNotebook;
        set
        {
            _selectedNotebook = value;
            OnPropertyChanged(nameof(SelectedNotebook));

            if (_selectedNotebook != null)
            {
                _ = GetNotesAsync();
            }
        }
    }

    private ObservableCollection<Note> _notes;
    public ObservableCollection<Note> Notes
    {
        get => _notes;
        set
        {
            _notes = value;
            OnPropertyChanged(nameof(Notes));
        }
    }

    private string _currentNoteXaml;
    public string CurrentNoteXaml
    {
        get => _currentNoteXaml;
        set
        {
            _currentNoteXaml = value;
            OnPropertyChanged(nameof(CurrentNoteXaml));
        }
    }

    private int _currentNoteCharCount;
    public int CurrentNoteCharCount
    {
        get => _currentNoteCharCount;
        set
        {
            _currentNoteCharCount = value;
            OnPropertyChanged(nameof(CurrentNoteCharCount));
            OnPropertyChanged(nameof(CurrentNoteCharCountStatusMessage));
        }
    }

    public string CurrentNoteCharCountStatusMessage
    {
        get => $"Document length: {CurrentNoteCharCount} characters";
    }

    public ICollection<FontFamily> AvailableFonts { get; }

    private FontFamily _selectedFont;
    public FontFamily SelectedFont
    {
        get => _selectedFont;
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
        get => _selectedFontSize;
        set
        {
            _selectedFontSize = value;
            OnPropertyChanged(nameof(SelectedFontSize));
        }
    }

    private Visibility _renameNotebookTextboxVisbility;
    public Visibility RenameNotebookTextboxVisibility
    {
        get => _renameNotebookTextboxVisbility;
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
    public ICommand DeleteNoteCommand { get; set; }
    public ICommand DeleteNotebookCommand { get; set; }

    public async Task CreateNotebookAsync()
    {
        Notebook notebook = new()
        {
            Name = "New notebook"
        };

        using NotesRepository db = NotesRepositoryFactory.CreateRepository();
        await db.CreateNotebook(notebook);
        await GetNotebooksAsync();
    }

    public async Task CreateNoteAsync(int notebookId)
    {
        Note newNote = new()
        {
            NotebookId = notebookId,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
            Title = "New note"
        };

        using NotesRepository db = NotesRepositoryFactory.CreateRepository();
        await db.CreateNote(newNote);
        await GetNotesAsync();
    }

    private async Task GetNotebooksAsync()
    {
        Notebooks.Clear();
        using NotesRepository db = NotesRepositoryFactory.CreateRepository();
        List<Notebook> notebooks = await db.GetAllNotebooks();
        notebooks.ForEach(x => Notebooks.Add(x));
    }

    private async Task GetNotesAsync()
    {
        if (SelectedNotebook != null)
        {
            Notes.Clear();
            using NotesRepository db = NotesRepositoryFactory.CreateRepository();
            List<Note> notes = await db.GetAllNotes(SelectedNotebook.Id);
            notes.ForEach(x => Notes.Add(x));
        }
    }

    public async Task DeleteNoteAsync(int noteId)
    {
        using NotesRepository db = NotesRepositoryFactory.CreateRepository();
        await db.DeleteNote(noteId);
        await GetNotesAsync();
    }

    public async Task DeleteNotebookAsync(int notebookId)
    {
        using NotesRepository db = NotesRepositoryFactory.CreateRepository();
        await db.DeleteNotebook(notebookId);
        await GetNotebooksAsync();
    }

    public void StartEditing()
    {
        RenameNotebookTextboxVisibility = Visibility.Visible;
    }

    public async Task StopEditingAsync(Notebook notebook)
    {
        RenameNotebookTextboxVisibility = Visibility.Collapsed;
        using NotesRepository db = NotesRepositoryFactory.CreateRepository();
        await db.UpdateNotebook(notebook);
    }
}
