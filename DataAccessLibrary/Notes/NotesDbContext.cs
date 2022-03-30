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
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        EntityTypeBuilder<User> user = modelBuilder.Entity<User>();
        EntityTypeBuilder<Notebook> notebook = modelBuilder.Entity<Notebook>();
        EntityTypeBuilder<Note> note = modelBuilder.Entity<Note>();

        //user.HasMany(u => u.Notebooks);  <-- Will add after implementing Users logins
        notebook.HasMany(n => n.Notes);
    }
}
