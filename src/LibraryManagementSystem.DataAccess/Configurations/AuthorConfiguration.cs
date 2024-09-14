using LibraryManagementSystem.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagementSystem.DataAccess.Configurations;

public class AuthorConfiguration : IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityColumn();
        
        builder.Property(x => x.FirstName).IsRequired().HasMaxLength(100);
        builder.Property(x => x.LastName).IsRequired().HasMaxLength(100);
        builder.Property(x => x.DateOfBirth).IsRequired();
        builder.Property(x => x.Nationality).IsRequired().HasMaxLength(100);

        builder.Property(x => x.CreatedBy).HasMaxLength(150);
        builder.Property(x => x.UpdatedBy).HasMaxLength(150);

        builder.HasOne(x => x.User).WithOne(x => x.Author).HasForeignKey<Author>(x => x.UserId);
    }
}
