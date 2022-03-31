using DataAccessLibrary.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessLibrary.Notes;

public class NotesDbContext : DbContext
{
    public NotesDbContext(DbContextOptions<NotesDbContext> options) : base(options)
    {

    }

    public DbSet<Note> Notes { get; set; }
    public DbSet<Notebook> Notebooks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        EntityTypeBuilder<Notebook> notebook = modelBuilder.Entity<Notebook>();
        EntityTypeBuilder<Note> note = modelBuilder.Entity<Note>();

        notebook.HasMany(n => n.Notes);
    }
}
