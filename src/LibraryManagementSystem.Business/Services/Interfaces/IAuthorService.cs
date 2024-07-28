using LibraryManagementSystem.Business.DTOs;
using LibraryManagementSystem.Business.DTOs.AuthorDtos;

namespace LibraryManagementSystem.Business.Services.Interfaces; 

public interface IAuthorService
{
    Task<List<AuthorGetDto>> GetAll();
    Task<AuthorGetByIdDto> GetByIdAsync(int id);
    Task<GenericResponseDto> CreateAsync(AuthorPostDto authorPostDto);
    Task<GenericResponseDto> UpdateAsync(AuthorPutDto authorPutDto);
    Task<GenericResponseDto> DeleteAsync(int id);
}
