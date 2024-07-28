using LibraryManagementSystem.Business.DTOs.BookAuthorDtos;

namespace LibraryManagementSystem.Business.DTOs.AuthorDtos;

public class AuthorPostDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Nationality { get; set; }
    public List<BookAuthorPostDto> BookAuthors { get; set; }
}
