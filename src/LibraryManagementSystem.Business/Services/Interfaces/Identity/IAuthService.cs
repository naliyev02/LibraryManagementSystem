using LibraryManagementSystem.Business.DTOs;
using LibraryManagementSystem.Business.DTOs.Identity.AuthDtos;
using LibraryManagementSystem.Business.DTOs.Identity.TokenDtos;

namespace LibraryManagementSystem.Business.Services.Interfaces.Identity;

public interface IAuthService
{
    Task<LoginResponse> LoginAsync(LoginDto authDto);
    Task<GenericResponseDto> RegisterAsync(RegisterDto registerDto);
    Task<GenericResponseDto> ForgotPasswordAsync(ForgotPasswordDto forgotPasswordDto);
    Task<GenericResponseDto> ResetPasswordAsync(ChangePasswordDto changePasswordDto);
    //Task<TokenDto> CreateTokenByRefreshTokenAsync(string refreshToken);
}
