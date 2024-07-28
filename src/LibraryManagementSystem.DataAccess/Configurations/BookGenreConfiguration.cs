using LibraryManagementSystem.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagementSystem.DataAccess.Configurations;

public class BookGenreConfiguration : IEntityTypeConfiguration<BookGenre>
{
    public void Configure(EntityTypeBuilder<BookGenre> builder)
    {
        //composite keys
        //builder.HasKey(x => new { x.BookId, x.GenreId });

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityColumn();

        builder.HasOne(x => x.Book).WithMany(y => y.BookGenres).HasForeignKey(x => x.BookId);
        builder.HasOne(x => x.Genre).WithMany(y => y.BookGenres).HasForeignKey(x => x.GenreId);
    }
}
