﻿using DataAccessLibrary.Entities;
using DataAccessLibrary.Notes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using WpfUI.Commands;

namespace WpfUI.ViewModels;

public class NotesViewModel : ViewModelBase
{
    private readonly NotesRepositoryFactory _notesRepositoryFactory;
    private readonly string _notesSavePath;

    public NotesViewModel(NotesRepositoryFactory notesRepositoryFactory)
    {
        _notesRepositoryFactory = notesRepositoryFactory;
        _notesSavePath = $@"{Environment.CurrentDirectory}\Notes";
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
        RenameNoteCommand = new RenameNoteCommand(this);
        UpdateNotebookCommand = new UpdateNotebookCommand(this);
        UpdateNoteCommand = new UpdateNoteCommand(this);
        DeleteNoteCommand = new DeleteNoteCommand(this);
        DeleteNotebookCommand = new DeleteNotebookCommand(this);
        SaveNoteCommand = new SaveNoteCommand(this);
        AvailableFonts = Fonts.SystemFontFamilies.OrderBy(f => f.Source).ToList();
        SelectedFont = AvailableFonts.FirstOrDefault(x => x.Source == "Segoe UI");
        AvailableFontSizes = Enumerable.Range(6, 72).ToList().ConvertAll(i => (double)i);
        SelectedFontSize = 14;
        Notebooks = new();
        Notes = new();
        _ = GetNotebooksAsync();
    }

    public ObservableCollection<Notebook> Notebooks { get; set; }
    public ObservableCollection<Note> Notes { get; set; }
    public List<double> AvailableFontSizes { get; set; }
    public ICollection<FontFamily> AvailableFonts { get; }

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

    private Note _selectedNote;
    public Note SelectedNote
    {
        get
        {
            return _selectedNote;
        }
        set
        {
            SaveNote();
            _selectedNote = value;
            OnPropertyChanged(nameof(SelectedNote));
            LoadNote();
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

    public ICommand NewNotebookCommand { get; private set; }
    public ICommand NewNoteCommand { get; private set; }
    public ICommand ExitApplicationCommand { get; private set; }
    public ICommand NoteTextChangedCommand { get; private set; }
    public ICommand BoldTextCommand { get; private set; }
    public ICommand ItalicTextCommand { get; private set; }
    public ICommand UnderlineTextCommand { get; private set; }
    public ICommand FontFamilyChangedCommand { get; private set; }
    public ICommand FontSizeChangedCommand { get; private set; }
    public ICommand RenameNotebookCommand { get; private set; }
    public ICommand RenameNoteCommand { get; private set; }
    public ICommand UpdateNotebookCommand { get; private set; }
    public ICommand UpdateNoteCommand { get; private set; }
    public ICommand DeleteNoteCommand { get; private set; }
    public ICommand DeleteNotebookCommand { get; private set; }
    public ICommand SaveNoteCommand { get; private set; }

    public async Task CreateNotebookAsync()
    {
        Notebook notebook = new()
        {
            Name = "New notebook"
        };

        using NotesRepository db = _notesRepositoryFactory.CreateRepository();
        int id = await db.CreateNotebook(notebook);
        await GetNotebooksAsync();
        SelectedNotebook = Notebooks.FirstOrDefault(x => x.Id == id);
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

        using NotesRepository db = _notesRepositoryFactory.CreateRepository();
        int id = await db.CreateNote(newNote);
        await GetNotesAsync();
        SelectedNote = Notes.FirstOrDefault(x => x.Id == id);
    }

    private async Task GetNotebooksAsync()
    {
        Notebook curNotebook = SelectedNotebook;
        Notebooks.Clear();
        using NotesRepository db = _notesRepositoryFactory.CreateRepository();
        List<Notebook> notebooks = await db.GetAllNotebooks();
        notebooks.ForEach(x => Notebooks.Add(x));

        if (curNotebook != null)
        {
            SelectedNotebook = Notebooks.FirstOrDefault(x => x.Id == curNotebook.Id);
        }
    }

    private async Task GetNotesAsync()
    {
        if (SelectedNotebook != null)
        {
            Notes.Clear();
            using NotesRepository db = _notesRepositoryFactory.CreateRepository();
            List<Note> notes = await db.GetAllNotes(SelectedNotebook.Id);
            notes.ForEach(x => Notes.Add(x));
        }
    }

    public async Task UpdateNotebookAsync()
    {
        using NotesRepository db = _notesRepositoryFactory.CreateRepository();
        await db.UpdateNotebook(SelectedNotebook);
    }

    public async Task UpdateNoteAsync()
    {
        using NotesRepository db = _notesRepositoryFactory.CreateRepository();
        await db.UpdateNote(SelectedNote);
    }

    public async Task DeleteNoteAsync(int noteId)
    {
        using NotesRepository db = _notesRepositoryFactory.CreateRepository();
        await db.DeleteNote(noteId);
        await GetNotesAsync();
    }

    public async Task DeleteNotebookAsync(int notebookId)
    {
        using NotesRepository db = _notesRepositoryFactory.CreateRepository();
        await db.DeleteNotebook(notebookId);
        await GetNotebooksAsync();
        Notes.Clear();
    }

    public void StartEditingNotebook()
    {
        SelectedNotebook.IsEditMode = true;
    }
    public async Task StopEditingNotebookAsync()
    {
        await UpdateNotebookAsync();
        SelectedNotebook.IsEditMode = false;
    }
    
    public void StartEditingNote()
    {
        SelectedNote.IsEditMode = true;
    }

    public async Task StopEditingNoteAsync()
    {
        using NotesRepository db = _notesRepositoryFactory.CreateRepository();
        Note existingNote = await db.GetNote(SelectedNote.Id);
        
        await UpdateNoteAsync();

        string existingFile = $@"{_notesSavePath}\{SelectedNotebook.Name}\{SelectedNote.Id}-{existingNote.Title}.rtf";
        string newFile = $@"{_notesSavePath}\{SelectedNotebook.Name}\{SelectedNote.Id}-{SelectedNote.Title}.rtf";

        if (File.Exists(existingFile))
        {
            File.Move(existingFile, newFile);
        }

        SelectedNote.IsEditMode = false;
    }

    public void LoadNote()
    {
        CurrentNoteXaml = "";

        if (SelectedNote != null && File.Exists(SelectedNote.FileLocation))
        {
            CurrentNoteXaml = File.ReadAllText(SelectedNote.FileLocation);
        }
    }

    public void SaveNote()
    {
        if (SelectedNote != null)
        {
            SelectedNote.FileLocation = $@"{_notesSavePath}\{SelectedNotebook.Name}\{SelectedNote.Id}-{SelectedNote.Title}.rtf";
            SelectedNote.UpdatedAt = DateTime.Now;
            _ = UpdateNoteAsync();
            Directory.CreateDirectory($@"{_notesSavePath}\{SelectedNotebook.Name}");
            File.WriteAllText(SelectedNote.FileLocation, CurrentNoteXaml);
        }
    }
}
