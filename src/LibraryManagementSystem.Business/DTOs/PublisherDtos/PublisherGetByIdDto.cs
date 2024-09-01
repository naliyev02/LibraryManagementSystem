using LibraryManagementSystem.Business.DTOs.BookDtos;

namespace LibraryManagementSystem.Business.DTOs.PublisherDtos;

public class PublisherGetByIdDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string ContactNumber { get; set; }
    public List<BookGetDto> Books { get; set; }
}
