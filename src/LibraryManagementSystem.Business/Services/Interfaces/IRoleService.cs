using LibraryManagementSystem.Business.DTOs.RoleDtos;
using LibraryManagementSystem.Business.DTOs;

namespace LibraryManagementSystem.Business.Services.Interfaces;

public interface IRoleService
{
    Task<IEnumerable<RoleGetDto>> GetAllRolesAsync();
    Task<GenericResponseDto> CreateRoleAsync(RolePostDto rolePostDto);
    Task<GenericResponseDto> DeleteRoleAsync(string name);
}
