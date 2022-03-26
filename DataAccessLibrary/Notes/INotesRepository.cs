using DataAccessLibrary.Entities;

namespace DataAccessLibrary.Notes;

public interface INotesRepository
{
    Task<int> CreateNote(Note newNote);
    Task<int> CreateNotebook(Notebook newNotebook);
    Task DeleteNote(int noteId);
    Task DeleteNotebook(int notebookId);
    Task<List<Notebook>> GetAllNotebooks();
    Task<List<Note>> GetAllNotes(int notebookId);
    Task UpdateNotebook(Notebook notebook);
}