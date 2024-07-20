using LibraryManagementSystem.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace LibraryManagementSystem.DataAccess.Configurations;

internal class BookAuthorConfiguration : IEntityTypeConfiguration<BookAuthor>
{
    public void Configure(EntityTypeBuilder<BookAuthor> builder)
    {
        //composite keys
        builder.HasKey(x => new {x.BookId, x.AuthorId });

        builder.HasOne(x => x.Book).WithMany(y => y.BookAuthors).HasForeignKey(x => x.BookId);
        builder.HasOne(x => x.Author).WithMany(y => y.BookAuthors).HasForeignKey(x => x.AuthorId);

    }
}
