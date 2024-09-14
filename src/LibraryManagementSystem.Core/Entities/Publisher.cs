using LibraryManagementSystem.Core.Entities.Common;
using LibraryManagementSystem.Core.Entities.Identity;

namespace LibraryManagementSystem.Core.Entities;

public class Publisher : BaseEntity
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string ContactNumber { get; set; }
     public virtual List<Book> Books { get; set; }

    public string? UserId { get; set; }
    public AppUser? User { get; set; }
}
