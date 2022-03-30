using Microsoft.EntityFrameworkCore;

namespace DataAccessLibrary.Notes;

public class NotesDbContextFactory
{
    private readonly string _connectionString;

    public NotesDbContextFactory(string connectionString)
    {
        _connectionString = connectionString;
    }

    public NotesDbContext CreateDbContext()
    {
        DbContextOptionsBuilder<NotesDbContext> options = new();
        options.UseSqlite(_connectionString);
        return new NotesDbContext(options.Options);
    }
}
