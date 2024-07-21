using LibraryManagementSystem.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagementSystem.DataAccess.Configurations;

public class PublisherConfiguration : IEntityTypeConfiguration<Publisher>
{
    public void Configure(EntityTypeBuilder<Publisher> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityColumn();

        builder.Property(x => x.Name).IsRequired().HasMaxLength(255);
        builder.Property(x => x.Address).IsRequired().HasMaxLength(255);
        builder.Property(x => x.ContactNumber).IsRequired().HasMaxLength(255);

        builder.HasMany(x => x.Books).WithOne(x => x.Publisher).HasForeignKey(x => x.PublisherId);
    }
}
