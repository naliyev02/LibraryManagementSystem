using LibraryManagementSystem.Business.DTOs.LanguageDtos;
using LibraryManagementSystem.Business.DTOs.PublisherDtos;

namespace LibraryManagementSystem.Business.Services.Interfaces;

public interface IPublisherService
{
    Task<List<PublisherGetDto>> GetAll();
}
