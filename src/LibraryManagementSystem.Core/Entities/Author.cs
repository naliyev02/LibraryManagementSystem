using LibraryManagementSystem.Core.Entities.Common;

namespace LibraryManagementSystem.Core.Entities;

public class Author : BaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Nationality { get; set; }
    List<BookAuthor> BookAuthors { get; set; }
}
