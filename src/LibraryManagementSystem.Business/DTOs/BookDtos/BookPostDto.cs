using LibraryManagementSystem.Business.DTOs.BookAuthorDtos;
using LibraryManagementSystem.Business.DTOs.BookGenreDtos;
using LibraryManagementSystem.Core.Entities;

namespace LibraryManagementSystem.Business.DTOs.BookDtos;

public class BookPostDto
{
    public string Title { get; set; }
    public string ISBN { get; set; }
    public int CoverTypeId { get; set; }
    public int LanguageId { get; set; }
    public int PageCount { get; set; }
    public int PublisherId { get; set; }
    public DateTime PublishedDate { get; set; }
    public int CopiesAvailable { get; set; }
    public List<BookAuthorPostDto> Authors { get; set; }
    public List<BookGenrePostDto> Genres { get; set; }
}
