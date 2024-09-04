using LibraryManagementSystem.Business.DTOs;
using LibraryManagementSystem.Business.DTOs.UserDtos;
using Microsoft.AspNetCore.Identity;

namespace LibraryManagementSystem.Business.Services.Interfaces;

public interface IUserService
{
    Task<IEnumerable<UserGetDto>> GetAllUsers();
    Task<UserGetDto> GetUserInfo();
    Task<GenericResponseDto> RegisterUserAsync(UserRegisterDto userRegisterDto);
    Task<GenericResponseDto> AddRoleToUserAsync(AddRoleToUserDto addRoleToUserDto);
}
