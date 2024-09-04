using LibraryManagementSystem.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagementSystem.DataAccess.Configurations;

public class CoverTypeConfiguration : IEntityTypeConfiguration<CoverType>
{
    public void Configure(EntityTypeBuilder<CoverType> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityColumn();

        builder.Property(x => x.Name).IsRequired().HasMaxLength(255);
        builder.HasMany(x => x.Books).WithOne(y => y.CoverType).HasForeignKey(y => y.CoverTypeId);

        builder.Property(x => x.CreatedBy).HasMaxLength(150);
        builder.Property(x => x.UpdatedBy).HasMaxLength(150);
    }
}
