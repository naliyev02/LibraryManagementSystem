using LibraryManagementSystem.Business.DTOs.RoleDtos;
using LibraryManagementSystem.Business.DTOs;

namespace LibraryManagementSystem.Business.Services.Interfaces;

public interface IRoleService
{
    Task<GenericResponseDto> CreateRoleAsync(RolePostDto rolePostDto);
}
