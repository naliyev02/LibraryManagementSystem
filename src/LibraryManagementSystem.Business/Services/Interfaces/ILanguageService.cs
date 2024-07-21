using LibraryManagementSystem.Business.DTOs;
using LibraryManagementSystem.Business.DTOs.LanguageDtos;

namespace LibraryManagementSystem.Business.Services.Interfaces;

public interface ILanguageService
{
    Task<List<LanguageGetDto>> GetAll();
    Task<LanguageGetDto> GetByIdAsync(int id);
    Task<GenericResponseDto> CreateAsync(LanguagePostDto languagePostDto);
    Task<GenericResponseDto> UpdateAsync(LanguagePutDto languagePutDto);
    Task<GenericResponseDto> DeleteAsync(int id);
}
