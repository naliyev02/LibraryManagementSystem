using LibraryManagementSystem.Business.DTOs.BookGenreDtos;

namespace LibraryManagementSystem.Business.DTOs.GenreDtos;

public class GenreGetByIdDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<GenreBookGetDto> Books { get; set; }
}