using LibraryManagementSystem.Business.DTOs.BookAuthorDtos;
using LibraryManagementSystem.Core.Entities;

namespace LibraryManagementSystem.Business.DTOs.AuthorDtos;

public class AuthorPutDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Nationality { get; set; }
    public List<AuthorBookPostDto> BookAuthors { get; set; }
}
