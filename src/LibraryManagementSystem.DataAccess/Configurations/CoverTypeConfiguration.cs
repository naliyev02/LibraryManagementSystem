using LibraryManagementSystem.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagementSystem.DataAccess.Configurations;

public class CoverTypeConfiguration : IEntityTypeConfiguration<CoverType>
{
    public void Configure(EntityTypeBuilder<CoverType> builder)
    {
        throw new NotImplementedException();
    }
}
