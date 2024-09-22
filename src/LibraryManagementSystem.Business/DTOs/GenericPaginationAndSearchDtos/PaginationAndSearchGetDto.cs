﻿namespace LibraryManagementSystem.Business.DTOs.GenericPaginationAndSearchDtos;

public class PaginationAndSearchGetDto<T>
{
    public List<T> Data { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }

    public int TotalPages { get; set; }
    public bool HasPrevious { get; set; }
    public bool HasNext { get; set; }
}
