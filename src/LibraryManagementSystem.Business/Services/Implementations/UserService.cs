using AutoMapper;
using LibraryManagementSystem.Business.DTOs;
using LibraryManagementSystem.Business.DTOs.UserDtos;
using LibraryManagementSystem.Business.Exceptions;
using LibraryManagementSystem.Business.Services.Interfaces;
using LibraryManagementSystem.Business.Utils.Enums;
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

    public async Task<IEnumerable<UserGetDto>> GetAllUsers()
    {
        var users =  _userManager.Users;

        var userDtos = _mapper.Map<IEnumerable<UserGetDto>>(users);

        return userDtos;
    }

    public async Task<UserGetDto> GetUserInfo()
    {
        var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            throw new GenericNotFoundException("User tapılmadı");

        var userDto = _mapper.Map<UserGetDto>(user);

        var roleDtos = await _userManager.GetRolesAsync(user);
        userDto.Roles = roleDtos.ToList();

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

        return new GenericResponseDto(200, "İstifadəçi uğurla yaradıldı");
    }

    public async Task<GenericResponseDto> AddRoleToUserAsync(AddRoleToUserDto addRoleToUserDto)
    {
        var user = await _userManager.FindByIdAsync(addRoleToUserDto.UserId);
        if (user == null)
        {
            throw new Exception("İstifadəçi tapılmadı.");
        }

        foreach (var role in addRoleToUserDto.Roles)
        {
            var result = await _userManager.AddToRoleAsync(user, role);
            if (!result.Succeeded)
                throw new Exception("Rolu əlavə etmək mümkün olmadı: " + string.Join(", ", result.Errors.Select(e => e.Description)));
        }

        return new GenericResponseDto(200, "İstifadəçiyə rol uğurla əlavə edildi");
    }

    public async Task<GenericResponseDto> UpdateRoleToUserAsync(UpdateRoleToUserDto updateRoleToUserDto)
    {
        var user = await _userManager.FindByIdAsync(updateRoleToUserDto.UserId);
        if (user == null)
        {
            throw new Exception("İstifadəçi tapılmadı.");
        }

        await _userManager.RemoveFromRolesAsync(user,Enum.GetNames(typeof(RoleType)));

        foreach (var role in updateRoleToUserDto.Roles)
        {
            var result = await _userManager.AddToRoleAsync(user, role);
            if (!result.Succeeded)
                throw new Exception("Rolu əlavə etmək mümkün olmadı: " + string.Join(", ", result.Errors.Select(e => e.Description)));
        }

        return new GenericResponseDto(200, "İstifadəçiyə rol uğurla əlavə edildi");
    }
}
