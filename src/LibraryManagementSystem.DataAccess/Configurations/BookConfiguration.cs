using LibraryManagementSystem.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagementSystem.DataAccess.Configurations;

public class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityColumn();

        builder.Property(x => x.Title).IsRequired().HasMaxLength(255);
        builder.Property(x => x.ISBN).HasMaxLength(255).IsRequired();
        builder.Property(x => x.PageCount).IsRequired();
        builder.Property(x => x.PublishedDate).IsRequired();
        builder.Property(x => x.CopiesAvailable).IsRequired();

    }
}
