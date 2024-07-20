using LibraryManagementSystem.Business.DTOs;
using LibraryManagementSystem.Business.DTOs.CoverTypeDtos;

namespace LibraryManagementSystem.Business.Services.Interfaces;

public interface ICoverTypeService
{
    Task<List<CoverTypeGetDto>> GetAll();
    Task<CoverTypeGetDto> GetByIdAsync(int id);
    Task<GenericResponseDto> CreateAsync(CoverTypePostDto coverTypePostDto);
    Task<GenericResponseDto> UpdateAsync(CoverTypePutDto coverTypePutDto);
    Task<GenericResponseDto> DeleteAsync(int id);
}
