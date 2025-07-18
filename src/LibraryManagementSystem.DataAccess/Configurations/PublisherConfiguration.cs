﻿using LibraryManagementSystem.Core.Entities;
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

        builder.Property(x => x.CreatedBy).HasMaxLength(150);
        builder.Property(x => x.UpdatedBy).HasMaxLength(150);

        builder.HasOne(x => x.User).WithOne(x => x.Publisher).HasForeignKey<Publisher>(x => x.UserId);
    }
}
