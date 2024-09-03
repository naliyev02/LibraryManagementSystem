using AutoMapper;
using LibraryManagementSystem.Business.DTOs;
using LibraryManagementSystem.Business.DTOs.UserDtos;
using LibraryManagementSystem.Business.Exceptions;
using LibraryManagementSystem.Business.Services.Interfaces;
using LibraryManagementSystem.Core.Entities.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace LibraryManagementSystem.Business.Services.Implementations;

public class UserService : IUserService
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

    public async Task<GenericResponseDto> RegisterUserAsync(UserRegisterDto userRegisterDto)
    {
        if (userRegisterDto.Password != userRegisterDto.ConfirmPassword)
            throw new ArgumentException("");

        var user = new AppUser
        {
            UserName = userRegisterDto.UserName,
            Email = userRegisterDto.Email,
        };

        var result = await _userManager.CreateAsync(user, userRegisterDto.Password);
        if (!result.Succeeded)
            throw new GenericNotFoundException("User yaradılarkən xəta baş verdi");

        return new GenericResponseDto(200, "User uğurla yaradıldı");
    }

    public async Task<GenericResponseDto> AddRoleToUserAsync(AddRoleToUserDto addRoleToUserDto)
    {
        var user = await _userManager.FindByIdAsync(addRoleToUserDto.userId);
        if (user == null)
        {
            throw new Exception("İstifadəçi tapılmadı.");
        }

        var result = await _userManager.AddToRoleAsync(user, addRoleToUserDto.roleName);
        if (!result.Succeeded)
            throw new Exception("Rolu əlavə etmək mümkün olmadı: " + string.Join(", ", result.Errors.Select(e => e.Description)));

        return new GenericResponseDto(200, "User uğurla yaradıldı");
    }
}
