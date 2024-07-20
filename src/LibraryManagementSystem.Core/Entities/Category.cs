using LibraryManagementSystem.Core.Entities.Common;

namespace LibraryManagementSystem.Core.Entities;

public class Category : BaseEntity
{
    public string Name { get; set; }
    public List<Genre> Genres { get; set; }
}
