namespace LibraryManagementSystem.Business.DTOs.ExtensionDtos.OrderByDtos;

public record OrderByRequestDto(string ColumnName, bool Ascending = true);

