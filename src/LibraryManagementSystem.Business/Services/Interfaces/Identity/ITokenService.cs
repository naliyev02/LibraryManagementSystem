using LibraryManagementSystem.Business.DTOs.Identity.TokenDtos;
using LibraryManagementSystem.Core.Entities.Identity;

namespace LibraryManagementSystem.Business.Services.Interfaces.Identity;

public interface ITokenService
{
    Task<TokenDto> GenerateTokenAsync(AppUser user);

}
