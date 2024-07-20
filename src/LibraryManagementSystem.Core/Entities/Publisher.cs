using LibraryManagementSystem.Core.Entities.Common;

namespace LibraryManagementSystem.Core.Entities;

public class Publisher : BaseEntity
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string ContactNumber { get; set; }
}
