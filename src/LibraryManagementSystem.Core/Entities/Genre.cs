using LibraryManagementSystem.Core.Entities.Common;

namespace LibraryManagementSystem.Core.Entities;

public class Genre : BaseEntity
{
    public string Name { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    List<BookGenre> BookGenres { get; set; }
}
