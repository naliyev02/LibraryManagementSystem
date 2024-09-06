using LibraryManagementSystem.Business.DTOs;
using LibraryManagementSystem.Business.DTOs.Identity.AuthDtos;
using LibraryManagementSystem.Business.DTOs.MailDtos;
using LibraryManagementSystem.Business.Exceptions;
using LibraryManagementSystem.Business.Services.Interfaces.Identity;
using LibraryManagementSystem.Business.Utils.Enums;
using LibraryManagementSystem.Business.Utils.Helpers;
using LibraryManagementSystem.Core.Entities.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;

namespace LibraryManagementSystem.Business.Services.Implementations.Identity;

public class AuthService : IAuthService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly ITokenService _tokenService;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly LinkGenerator _linkGenerator;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthService(UserManager<AppUser> userManager, ITokenService tokenService, SignInManager<AppUser> signInManager, LinkGenerator linkGenerator, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment webHostEnvironment)
    {
        _userManager = userManager;
        _tokenService = tokenService;
        _signInManager = signInManager;
        _linkGenerator = linkGenerator;
        _httpContextAccessor = httpContextAccessor;
        _webHostEnvironment = webHostEnvironment;
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

    public async Task<GenericResponseDto> RegisterAsync(RegisterDto registerDto)
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

        await _userManager.AddToRoleAsync(user, RoleType.Member.ToString());

        return new GenericResponseDto(200, "İstifadəçi uğurla əlavə edildi");
    }

    public async Task<GenericResponseDto> ForgotPasswordAsync(ForgotPasswordDto forgotPasswordDto)
    {
        var user = await _userManager.FindByEmailAsync(forgotPasswordDto.Email);
        if (user is null)
            throw new GenericNotFoundException("Istifadəçi tapılmadı");

        //https://localhost:7021/Account/ResetPassword?userId&Token //numune olaraq yazilib

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);

        var httpContext = _httpContextAccessor.HttpContext;
        string url = _linkGenerator.GetUriByAction(httpContext, "ResetPassword", "Account", new { userId = user.Id, token });

        EmailHelper emailHelper = new EmailHelper();

        string body = await GetEmailTemplateAsync(url);

        MailPostDto mailPostDto = new()
        {
            ToEmail = user.Email,
            Subject = "Reset your password",
            Body = body
        };

        await emailHelper.SendEmailAsync(mailPostDto);

        return new GenericResponseDto(200, "Mail gönderildi");
    }

    public async Task<GenericResponseDto> ResetPasswordAsync(ChangePasswordDto changePasswordDto)
    {
        if (string.IsNullOrWhiteSpace(changePasswordDto.UserId) || string.IsNullOrWhiteSpace(changePasswordDto.Token))
            throw new ArgumentNullException();

        var user = await _userManager.FindByIdAsync(changePasswordDto.UserId);
        if (user is null)
            throw new GenericNotFoundException("Istifadəçi tapılmadı");

        var identityResult = await _userManager.ResetPasswordAsync(user, changePasswordDto.Token, changePasswordDto.Password);

        if (!identityResult.Succeeded)
            throw new Exception("User registration failed: " + string.Join(", ", identityResult.Errors.Select(e => e.Description)));

        return new GenericResponseDto(200, "Şifrə uğurla yeniləndi");
    }


    private async Task<string> GetEmailTemplateAsync(string url)
    {
        string path = Path.Combine(_webHostEnvironment.WebRootPath, "assets", "templates", "template.html");

        StreamReader streamReader = new StreamReader(path);
        string result = await streamReader.ReadToEndAsync();
        result = result.Replace("[reset_password_url]", url);
        streamReader.Close();

        return result;
    }

}
