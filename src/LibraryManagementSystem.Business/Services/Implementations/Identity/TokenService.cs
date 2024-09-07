using LibraryManagementSystem.Business.DTOs.Identity.AuthDtos;
using LibraryManagementSystem.Business.DTOs.Identity.ClaimDtos;
using LibraryManagementSystem.Business.DTOs.Identity.TokenDtos;
using LibraryManagementSystem.Business.Services.Interfaces.Identity;
using LibraryManagementSystem.Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace LibraryManagementSystem.Business.Services.Implementations.Identity;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<AppRole> _roleManager;

    public TokenService(IConfiguration configuration, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
    {
        _configuration = configuration;
        _userManager = userManager;
        _roleManager = roleManager;
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
        var roles = (await _userManager.GetRolesAsync(user)).ToList();

        response.IsLogedIn = true;
        response.JwtToken = this.GenerateTokenString(new ClaimDto() { Id = user.Id, UserName = user.UserName, Roles = roles });
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
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:SecurityKey").Value));

        //var securityKey = GetRsaKey();

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

    private string GenerateTokenString(ClaimDto claimDto)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name,claimDto.UserName),
            new Claim(ClaimTypes.NameIdentifier, claimDto.Id),
        };

        foreach (var role in claimDto.Roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        //var staticKey = _configuration.GetSection("Jwt:SecurityKey").Value;
        //var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8
        //    .GetBytes(_configuration["Jwt:SecurityKey"]));
        //var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha512Signature);

        RsaSecurityKey rsaSecurityKey = GetRsaKey();
        var signingCredentials = new SigningCredentials(rsaSecurityKey, SecurityAlgorithms.RsaSha256);

        var securityToken = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            notBefore: DateTime.UtcNow,
            expires: DateTime.UtcNow.AddMinutes(20),
            signingCredentials: signingCredentials
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
