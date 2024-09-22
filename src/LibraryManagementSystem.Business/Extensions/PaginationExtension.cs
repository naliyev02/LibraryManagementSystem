using LibraryManagementSystem.Business.DTOs.ExtensionDtos.PaginationDtos;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Business.Extensions;

public static class PaginationExtension
{

    public static async Task<PaginationResponseDto<T>> Pagination<T> (this IQueryable<T> query, PaginationRequestDto request)
    {
        var count = await query.CountAsync();

        var list = await query
            .Skip((request.CurrentPage - 1) * request.Size)
            .Take(request.Size)
            .ToListAsync();

        var paginationResult = new PaginationResponseDto<T>(list, request.CurrentPage, request.Size, count);

        return paginationResult;
    }
}
