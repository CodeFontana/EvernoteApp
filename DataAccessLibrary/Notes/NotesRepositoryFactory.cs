namespace DataAccessLibrary.Notes;

public class NotesRepositoryFactory
{
    public static NotesRepository CreateRepository()
    {
        NotesDbContext db = NotesDbContextFactory.CreateDbContext();
        return new NotesRepository(db);
    }
}
