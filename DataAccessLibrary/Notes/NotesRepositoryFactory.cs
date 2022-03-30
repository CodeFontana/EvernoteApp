namespace DataAccessLibrary.Notes;

public class NotesRepositoryFactory
{
    private readonly NotesDbContextFactory _notesDbContextFactory;

    public NotesRepositoryFactory(NotesDbContextFactory notesDbContextFactory)
    {
        _notesDbContextFactory = notesDbContextFactory;
    }

    public NotesRepository CreateRepository()
    {
        NotesDbContext db = _notesDbContextFactory.CreateDbContext();
        return new NotesRepository(db);
    }
}
