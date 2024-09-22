using LibraryManagementSystem.Business.DTOs.CoverTypeDtos;

namespace LibraryManagementSystem.Business.DTOs.BookDtos;

public class BookGetDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string ISBN { get; set; }
    public int CopiesAvailable { get; set; }

    public CoverTypeGetDto CoverType { get; set; }
}
