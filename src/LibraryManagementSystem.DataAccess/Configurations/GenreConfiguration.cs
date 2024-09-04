using LibraryManagementSystem.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagementSystem.DataAccess.Configurations;

public class GenreConfiguration : IEntityTypeConfiguration<Genre>
{
    public void Configure(EntityTypeBuilder<Genre> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityColumn();

        builder.Property(x => x.Name).IsRequired().HasMaxLength(255);
        builder.HasOne(x => x.Category).WithMany(x => x.Genres).HasForeignKey(x => x.CategoryId);

        builder.Property(x => x.CreatedBy).HasMaxLength(150);
        builder.Property(x => x.UpdatedBy).HasMaxLength(150);
    }
}
