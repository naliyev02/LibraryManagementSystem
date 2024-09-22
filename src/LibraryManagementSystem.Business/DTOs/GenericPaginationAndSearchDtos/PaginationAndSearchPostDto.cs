using LibraryManagementSystem.Business.DTOs.ExtensionDtos.OrderByDtos;
using LibraryManagementSystem.Business.DTOs.ExtensionDtos.PaginationDtos;
using LibraryManagementSystem.Business.DTOs.ExtensionDtos.SearchDtos;

namespace LibraryManagementSystem.Business.DTOs.GenericPaginationAndSearchDtos;

public class PaginationAndSearchPostDto
{
    public List<SearchRequestDto>? Searchs { get; set; }
    public OrderByRequestDto? OrderBy { get; set; }
    public PaginationRequestDto? Pagination { get; set; }
}
