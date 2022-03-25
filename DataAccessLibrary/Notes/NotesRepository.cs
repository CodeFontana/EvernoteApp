﻿using DataAccessLibrary.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLibrary.Notes;

public class NotesRepository : INotesRepository, IDisposable
{
    private readonly NotesDbContext _db;

    public NotesRepository(NotesDbContext notesDbContext)
    {
        _db = notesDbContext;
    }

    public async Task CreateNotebook(Notebook newNotebook)
    {
        await _db.Notebooks.AddAsync(newNotebook);
        await _db.SaveChangesAsync();
    }

    public async Task CreateNote(Note newNote)
    {
        await _db.Notes.AddAsync(newNote);
        await _db.SaveChangesAsync();
    }

    public async Task<List<Notebook>> GetAllNotebooks()
    {
        return await _db.Notebooks.ToListAsync();
    }

    public async Task<List<Note>> GetAllNotes(int notebookId)
    {
        return await _db.Notes.Where(x => x.NotebookId == notebookId).ToListAsync();
    }

    public async Task UpdateNotebook(Notebook notebook)
    {
        Notebook nb = _db.Notebooks.FirstOrDefault(x => x.Id == notebook.Id);

        if (nb != null)
        {
            nb.Name = notebook.Name;
            await _db.SaveChangesAsync();
        }
    }

    public async Task DeleteNote(int noteId)
    {
        Note note = _db.Notes.FirstOrDefault(x => x.Id == noteId);

        if (note != null)
        {
            _db.Notes.Remove(note);
            await _db.SaveChangesAsync();
        }
    }

    public async Task DeleteNotebook(int notebookId)
    {
        IQueryable<Note> notes = _db.Notes.Where(x => x.NotebookId == notebookId);
        notes?.ToList().ForEach(async n => await DeleteNote(n.Id));
        Notebook notebook = _db.Notebooks.FirstOrDefault(n => n.Id == notebookId);

        if (notebook != null)
        {
            _db.Notebooks.Remove(notebook);
            await _db.SaveChangesAsync();
        }
    }

    public void Dispose()
    {
        _db?.Dispose();
    }
}