using LibraryManagementSystem.Core.Entities;
using LibraryManagementSystem.Core.Entities.Common;
using LibraryManagementSystem.DataAccess.Configurations;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.DataAccess.Contexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Book> Books { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<BookGenre> BookGenres { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<CoverType> CoverTypes { get; set; }
    public DbSet<Language> Languages { get; set; }
    public DbSet<Publisher> Publishers { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<BookAuthor> BookAuthors { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BookConfiguration).Assembly);

        modelBuilder.Entity<Book>().HasQueryFilter(p => !p.IsDeleted);

        base.OnModelCreating(modelBuilder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker.Entries<BaseEntity>();

        foreach (var entry in entries)
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                    entry.Entity.CreatedBy = 1;
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                    entry.Entity.UpdatedBy = 1;
                    break;

                case EntityState.Modified:
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                    entry.Entity.UpdatedBy = 1;
                    break;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}
