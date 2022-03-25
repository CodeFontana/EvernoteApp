using Microsoft.EntityFrameworkCore;

namespace DataAccessLibrary.Notes;

public class NotesDbContextFactory
{
    public static NotesDbContext CreateDbContext()
    {
        DbContextOptionsBuilder<NotesDbContext> options = new();
        options.UseSqlite($@"Data Source={Environment.CurrentDirectory}\Notes.db;");
        return new NotesDbContext(options.Options);
    }
}
