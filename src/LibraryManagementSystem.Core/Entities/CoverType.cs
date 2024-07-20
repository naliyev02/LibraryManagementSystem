using LibraryManagementSystem.Core.Entities.Common;

namespace LibraryManagementSystem.Core.Entities;

public class CoverType : BaseEntity
{
    public string Name { get; set; }
    public Book Book { get; set; }
}
