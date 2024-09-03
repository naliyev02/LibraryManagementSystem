using LibraryManagementSystem.Business.DTOs.UserDtos;
using Microsoft.AspNetCore.Identity;

namespace LibraryManagementSystem.Business.Services.Interfaces;

public interface IUserService
{
    Task<UserGetDto> GetUser();
    Task<IdentityResult> RegisterUserAsync(UserRegisterDto userRegisterDto);
    Task<IdentityResult> AddRoleToUserAsync(string userId, string roleName);
}
