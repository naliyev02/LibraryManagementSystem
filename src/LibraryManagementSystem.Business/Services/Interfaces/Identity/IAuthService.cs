using LibraryManagementSystem.Business.DTOs.Identity.AuthDtos;
using LibraryManagementSystem.Business.DTOs.Identity.TokenDtos;

namespace LibraryManagementSystem.Business.Services.Interfaces.Identity;

public interface IAuthService
{
    Task<TokenDto> LoginAsync(LoginDto authDto);
    Task RegisterAsync(RegisterDto registerDto);
    Task<TokenDto> CreateTokenByRefreshTokenAsync(string refreshToken);
}
