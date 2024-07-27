using LibraryManagementSystem.Business.DTOs.AuthorDtos;

namespace LibraryManagementSystem.Business.Services.Interfaces; 

public interface IAuthorService
{
    Task<List<AuthorGetDto>> GetAll();
    Task<AuthorGetByIdDto> GetByIdAsync(int id);
}
