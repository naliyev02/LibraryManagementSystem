using LibraryManagementSystem.Core.Entities.Common;

namespace LibraryManagementSystem.Core.Entities;

public class Book : BaseEntity
{
    public string Title { get; set; }
    public string ISBN { get; set; }
    //public int AuthorId { get; set; }
    //public Author Author { get; set; }
    public int CoverTypeId { get; set; }
    public CoverType CoverType { get; set; }
    public int LanguageId { get; set; }
    public Language Language { get; set; }
    public int PageCount { get; set; }
    public int PublisherId { get; set; }
    public Publisher Publisher { get; set; }
    public DateTime PublishedDate { get; set; }
    public int CopiesAvailable { get; set; }
    List<BookAuthor> BookAuthors { get; set; }
    List<BookGenre> BookGenres { get; set; }
}

