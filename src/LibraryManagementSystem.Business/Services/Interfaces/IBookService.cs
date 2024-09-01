using LibraryManagementSystem.Business.DTOs;
using LibraryManagementSystem.Business.DTOs.BookDtos;

namespace LibraryManagementSystem.Business.Services.Interfaces;

public interface IBookService
{
    Task<List<BookGetDto>> GetAll();
    Task<BookGetByIdDto> GetByIdAsync(int id);
    Task<GenericResponseDto> CreateAsync(BookPostDto bookPostDto);
    Task<GenericResponseDto> UpdateAsync(BookPutDto bookPutDto);
    Task<GenericResponseDto> DeleteAsync(int id);
}
