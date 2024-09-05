using LibraryManagementSystem.Business.DTOs.Identity.AuthDtos;
using LibraryManagementSystem.Business.DTOs.Identity.TokenDtos;
using LibraryManagementSystem.Business.Services.Interfaces.Identity;
using LibraryManagementSystem.Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace LibraryManagementSystem.Business.Services.Implementations.Identity;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;
    private readonly UserManager<AppUser> _userManager;

    public TokenService(IConfiguration configuration, UserManager<AppUser> userManager)
    {
        _configuration = configuration;
        _userManager = userManager;
    }

    public async Task<LoginResponse> RefreshToken(TokenDto model)
    {
        var principal = GetTokenPrincipal(model.JwtToken);

        var response = new LoginResponse();
        if (principal?.Identity?.Name is null)
            return response;

        var identityUser = await _userManager.FindByNameAsync(principal.Identity.Name);

        if (identityUser is null || identityUser.RefreshToken != model.RefreshToken || identityUser.RefreshTokenExpiry < DateTime.Now)
            return response;

        await GeneratetokensAndUpdatetSataBase(response, identityUser);

        return response;
    }

    public async Task GeneratetokensAndUpdatetSataBase(LoginResponse response, AppUser? user)
    {
        response.IsLogedIn = true;
        response.JwtToken = this.GenerateTokenString(user.Id, user.UserName);
        response.RefreshToken = this.GenerateRefreshToken();

        user.RefreshToken = response.RefreshToken;
        user.RefreshTokenExpiry = DateTime.Now.AddHours(12);

        await _userManager.SetAuthenticationTokenAsync(user, "Local", "Access", response.JwtToken);
        await _userManager.UpdateAsync(user);
    }



    private string GenerateRefreshToken()
    {
        var numberByte = new byte[32];
        using var random = RandomNumberGenerator.Create();
        random.GetBytes(numberByte);

        return Convert.ToBase64String(numberByte);
    }

    private ClaimsPrincipal? GetTokenPrincipal(string token)
    {
        //var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("Jwt:Key").Value));

        var securityKey = GetRsaKey();

        var validation = new TokenValidationParameters
        {
            IssuerSigningKey = securityKey,
            ValidateLifetime = false,
            ValidateActor = false,
            ValidateIssuer = false,
            ValidateAudience = false,
        };

        return new JwtSecurityTokenHandler().ValidateToken(token, validation, out _);
    }

    private string GenerateTokenString(string id, string userName)
    {
        var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,userName),
                new Claim(ClaimTypes.NameIdentifier, id)
            };

        //var staticKey = _config.GetSection("Jwt:Key").Value;
        //var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(staticKey));
        //var signingCred = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

        RsaSecurityKey rsaSecurityKey = GetRsaKey();
        var signingCred = new SigningCredentials(rsaSecurityKey, SecurityAlgorithms.RsaSha256);

        var securityToken = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddMinutes(20),
            signingCredentials: signingCred
            );

        string tokenString = new JwtSecurityTokenHandler().WriteToken(securityToken);
        return tokenString;
    }

    private RsaSecurityKey GetRsaKey()
    {
        var rsaKey = RSA.Create();
        string xmlKey = File.ReadAllText(_configuration.GetSection("Jwt:PrivateKeyPath").Value);
        rsaKey.FromXmlString(xmlKey);
        var rsaSecurityKey = new RsaSecurityKey(rsaKey);
        return rsaSecurityKey;
    }

}
