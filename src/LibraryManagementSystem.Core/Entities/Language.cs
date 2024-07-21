using LibraryManagementSystem.Core.Entities.Common;

namespace LibraryManagementSystem.Core.Entities;

public class Language : BaseEntity
{
    public string Name { get; set; }
    public List<Book> Books { get; set; }
}
