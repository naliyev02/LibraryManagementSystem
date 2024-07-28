using LibraryManagementSystem.Core.Entities.Common;

namespace LibraryManagementSystem.Core.Entities;

public class BookGenre : BaseEntity
{
    //public int Id { get; set; }
    public int BookId { get; set; }
    public Book Book { get; set; }
    public int GenreId { get; set; }
    public Genre Genre { get; set; }
}
