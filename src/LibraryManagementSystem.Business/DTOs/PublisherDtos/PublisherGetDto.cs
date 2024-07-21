using LibraryManagementSystem.Core.Entities;

namespace LibraryManagementSystem.Business.DTOs.PublisherDtos;

public class PublisherGetDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string ContactNumber { get; set; }
    public List<PublisherBookGetDtos> Books { get; set; }

}
