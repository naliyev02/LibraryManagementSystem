using LibraryManagementSystem.Business.DTOs;
using LibraryManagementSystem.Business.DTOs.CategoryDtos;

namespace LibraryManagementSystem.Business.Services.Interfaces;

public interface ICategoryService
{
    Task<List<CategoryGetDto>> GetAll();
    Task<CategoryGetDto> GetByIdAsync(int id);
    Task<GenericResponseDto> CreateAsync(CategoryPostDto categoryPostDto);
    Task<GenericResponseDto> UpdateAsync(CategoryPutDto categoryPutDto);
    Task<GenericResponseDto> DeleteAsync(int id);
}
