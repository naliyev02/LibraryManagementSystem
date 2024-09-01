using LibraryManagementSystem.Business.DTOs;
using LibraryManagementSystem.Business.DTOs.GenreDtos;

namespace LibraryManagementSystem.Business.Services.Interfaces;

public interface IGenreService
{
    Task<List<GenreGetDto>> GetAll();
    Task<GenreGetByIdDto> GetByIdAsync(int id);
    Task<GenericResponseDto> CreateAsync(GenrePostDto genrePostDto);
    Task<GenericResponseDto> UpdateAsync(GenrePutDto genrePutDto);
    Task<GenericResponseDto> DeleteAsync(int id);

}
