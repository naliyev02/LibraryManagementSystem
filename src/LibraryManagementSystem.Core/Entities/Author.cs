using LibraryManagementSystem.Core.Entities.Common;
using LibraryManagementSystem.Core.Entities.Identity;

namespace LibraryManagementSystem.Core.Entities;

public class Author : BaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Nationality { get; set; }
    public List<BookAuthor> BookAuthors { get; set; }

    public string? UserId { get; set; }
    public AppUser? User { get; set; }
}
