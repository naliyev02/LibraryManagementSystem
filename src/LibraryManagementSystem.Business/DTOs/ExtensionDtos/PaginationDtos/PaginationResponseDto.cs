﻿namespace LibraryManagementSystem.Business.DTOs.ExtensionDtos.PaginationDtos;

public record PaginationResponseDto<T>(List<T> Data, int PageNumber, int PageSize, int TotalCount)
{
    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
    public bool HasPrevious => PageNumber > 1;
    public bool HasNext => PageNumber < TotalPages;
}
