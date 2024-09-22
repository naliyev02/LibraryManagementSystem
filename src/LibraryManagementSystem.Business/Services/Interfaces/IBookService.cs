using LibraryManagementSystem.Business.DTOs;
using LibraryManagementSystem.Business.DTOs.BookDtos;
using LibraryManagementSystem.Business.DTOs.ExtensionDtos.OrderByDtos;
using LibraryManagementSystem.Business.DTOs.ExtensionDtos.SearchDtos;
using LibraryManagementSystem.Business.DTOs.GenericPaginationAndSearchDtos;

namespace LibraryManagementSystem.Business.Services.Interfaces;

public interface IBookService
{
    Task<List<BookGetDto>> GetAll();
    Task<PaginationAndSearchGetDto<BookGetDto>> GetWithPaginationAndSearch(PaginationAndSearchPostDto genericPaginationAndSearchDto);
    Task<BookGetByIdDto> GetByIdAsync(int id);
    Task<GenericResponseDto> CreateAsync(BookPostDto bookPostDto);
    Task<GenericResponseDto> UpdateAsync(BookPutDto bookPutDto);
    Task<GenericResponseDto> DeleteAsync(int id);
}
