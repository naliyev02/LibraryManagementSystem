using LibraryManagementSystem.Core.Entities;

namespace LibraryManagementSystem.Business.DTOs.BookAuthorDtos;

public class BookAuthorGetDto
{
    public AuthorBookGetDto Author { get; set; }
}
