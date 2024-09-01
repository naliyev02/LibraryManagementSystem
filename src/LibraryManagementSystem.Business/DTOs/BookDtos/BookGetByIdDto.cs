using LibraryManagementSystem.Business.DTOs.BookAuthorDtos;
using LibraryManagementSystem.Business.DTOs.CoverTypeDtos;
using LibraryManagementSystem.Business.DTOs.LanguageDtos;
using LibraryManagementSystem.Business.DTOs.PublisherDtos;
using LibraryManagementSystem.Core.Entities;

namespace LibraryManagementSystem.Business.DTOs.BookDtos;

public class BookGetByIdDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string ISBN { get; set; }
    public CoverTypeGetDto CoverType { get; set; }
    public LanguageGetDto Language { get; set; }
    public int PageCount { get; set; }
    public PublisherGetDto Publisher { get; set; }
    public DateTime PublishedDate { get; set; }
    public int CopiesAvailable { get; set; }
    public List<BookAuthorGetDto> BookAuthors { get; set; }
    public List<BookGenre> BookGenres { get; set; }
}
