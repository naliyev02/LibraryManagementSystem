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

    public async Task<LoginResponse> LoginAsync(LoginDto authDto)
    {
        var user = await _userManager.FindByNameAsync(authDto.UserName);
        if (user is null)
            throw new GenericNotFoundException("Invalid username or password");

        bool isSuccess = await _userManager.CheckPasswordAsync(user, authDto.Password);
        if (!isSuccess)
            throw new GenericNotFoundException("Invalid username or password");


        LoginResponse response = new LoginResponse();

        await _tokenService.GeneratetokensAndUpdatetSataBase(response,user);

        return response;
    }

    //public async Task<TokenDto> CreateTokenByRefreshTokenAsync(string refreshToken)
    //{

    //    //var userRefreshToken = _identityUserToken.Name.Equals(refreshToken);
    //    //string userId = null;

    //    //if (!userRefreshToken)
    //    //    throw new GenericNotFoundException("");
    //    //else
    //    //{
    //    //    userId = _identityUserToken.UserId;
    //    //}

    //    //var user = await _userManager.FindByIdAsync(userId);
    //    //if (user == null)
    //    //    throw new GenericNotFoundException("");

    //    //TokenDto token = await _tokenService.GenerateTokenAsync(user);

    //    //await _userManager.SetAuthenticationTokenAsync(user, "Local", "AccessToken", token.Token);
    //    //await _userManager.SetAuthenticationTokenAsync(user, "Local", "RefreshToken", token.RefreshToken);

    //    //return token;

    //    return new();
    //}

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
