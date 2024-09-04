using LibraryManagementSystem.Business.DTOs.Identity.AuthDtos;
using LibraryManagementSystem.Business.DTOs.Identity.TokenDtos;
using LibraryManagementSystem.Business.Exceptions;
using LibraryManagementSystem.Business.Services.Interfaces.Identity;
using LibraryManagementSystem.Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace LibraryManagementSystem.Business.Services.Implementations.Identity;

public class AuthService : IAuthService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly ITokenService _tokenService;

    public AuthService(UserManager<AppUser> userManager, ITokenService tokenService)
    {
        _userManager = userManager;
        _tokenService = tokenService;
    }

    public async Task<TokenDto> LoginAsync(LoginDto authDto)
    {
        var user = await _userManager.FindByNameAsync(authDto.UserName);
        if (user is null)
            throw new GenericNotFoundException("Invalid username or password");

        bool isSuccess = await _userManager.CheckPasswordAsync(user, authDto.Password);
        if (!isSuccess)
            throw new GenericNotFoundException("Invalid username or password");

        TokenDto token = await _tokenService.GenerateTokenAsync(user);

        await _userManager.SetAuthenticationTokenAsync(user, "Local", "AccessToken", token.Token);

        return token;
    }

    public async Task RegisterAsync(RegisterDto registerDto)
    {
        if (registerDto.Password != registerDto.ConfirmPassword)
            throw new ArgumentException("Passwords do not match");

        var user = new AppUser
        {
            UserName = registerDto.UserName,
            Email = registerDto.Email
        };

        var result = await _userManager.CreateAsync(user, registerDto.Password);
        if (!result.Succeeded)
            throw new Exception("User registration failed: " + string.Join(", ", result.Errors.Select(e => e.Description)));
    }
}
