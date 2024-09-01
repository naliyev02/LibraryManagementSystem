using LibraryManagementSystem.Business.DTOs.BookAuthorDtos;

namespace LibraryManagementSystem.Business.DTOs.AuthorDtos;

public class AuthorGetByIdDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Nationality { get; set; }
    public List<AuthorBookGetDto> Books { get; set; }
}
