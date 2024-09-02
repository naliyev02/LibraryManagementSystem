using AutoMapper;
using LibraryManagementSystem.Business.DTOs.UserDtos;
using LibraryManagementSystem.Core.Entities.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace LibraryManagementSystem.Business.Services.Implementations;

public class UserService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IMapper _mapper;

    public UserService(UserManager<AppUser> userManager, IHttpContextAccessor httpContextAccessor, IMapper mapper)
    {
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
        _mapper = mapper;
    }

    public async Task<UserGetDto> GetUser()
    {
        var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

        var user = await _userManager.FindByIdAsync(userId);
        var userDto = _mapper.Map<UserGetDto>(user);

        return userDto;
    }
}
