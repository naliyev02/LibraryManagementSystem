using LibraryManagementSystem.Business.DTOs;
using LibraryManagementSystem.Business.DTOs.Identity.AuthDtos;
using LibraryManagementSystem.Business.DTOs.Identity.TokenDtos;
using LibraryManagementSystem.Core.Entities.Identity;

namespace LibraryManagementSystem.Business.Services.Interfaces.Identity;

public interface IAuthService
{
    Task<LoginResponse> LoginAsync(LoginDto authDto);
    Task<GenericResponseDto> RegisterAsync(RegisterDto registerDto);
    Task<GenericResponseDto> ForgotPasswordAsync(ForgotPasswordDto forgotPasswordDto);
    Task<GenericResponseDto> ResetPasswordWithResetTokenAsync(ResetPasswordDto resetPasswordDto);
    Task<GenericResponseDto> ChangePasswordAsync(ChangePasswordDto changePasswordDto);
}
