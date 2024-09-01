using LibraryManagementSystem.Business.DTOs;
using LibraryManagementSystem.Business.DTOs.LanguageDtos;
using LibraryManagementSystem.Business.DTOs.PublisherDtos;

namespace LibraryManagementSystem.Business.Services.Interfaces;

public interface IPublisherService
{
    Task<List<PublisherGetDto>> GetAll();
    Task<PublisherGetByIdDto> GetByIdAsync(int id);
    Task<GenericResponseDto> CreateAsync(PublisherPostDto publisherPostDto);
    Task<GenericResponseDto> UpdateAsync(PublisherPutDto publisherPutDto);
    Task<GenericResponseDto> DeleteAsync(int id);
}
