using AutoMapper;
using LibraryManagementSystem.Business.DTOs;
using LibraryManagementSystem.Business.DTOs.Identity.AuthDtos;
using LibraryManagementSystem.Business.DTOs.MailDtos;
using LibraryManagementSystem.Business.Exceptions;
using LibraryManagementSystem.Business.Services.Interfaces;
using LibraryManagementSystem.Business.Services.Interfaces.Identity;
using LibraryManagementSystem.Business.Utils.Enums;
using LibraryManagementSystem.Business.Utils.Helpers;
using LibraryManagementSystem.Core.Entities.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Security.Principal;

namespace LibraryManagementSystem.Business.Services.Implementations.Identity;

public class AuthService : IAuthService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly ITokenService _tokenService;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IUserService _userService;
    private readonly IMapper _mapper;
    


    public AuthService(UserManager<AppUser> userManager, ITokenService tokenService, IWebHostEnvironment webHostEnvironment, IUserService userService, IMapper mapper)
    {
        _userManager = userManager;
        _tokenService = tokenService;
        _webHostEnvironment = webHostEnvironment;
        _userService = userService;
        _mapper = mapper;
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

        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

        var verifyUrl = $"https://localhost:7211/Users/Register?{user.Id}&{token}";

        //https://localhost:7021/Users/Register?userId&Token //numune olaraq yazilib

        string body = await this.GetEmailTemplateAsync(verifyUrl);

        MailPostDto mailPostDto = new()
        {
            ToEmail = user.Email,
            Subject = "Reset your password",
            Body = body
        };

        await EmailHelper.SendEmailAsync(mailPostDto);

        return new GenericResponseDto(200, "İstifadəçi uğurla əlavə edildi");
    }

    public async Task<GenericResponseDto> ConfirmRegister(ConfirmRegisterDto confirmRegisterDto)
    {
        var user = await _userManager.FindByIdAsync(confirmRegisterDto.UserId);
        if (user == null)
            throw new GenericNotFoundException("Istifadəçi tapılmadı");

        var result = await _userManager.ConfirmEmailAsync(user, confirmRegisterDto.Token);
        if (!result.Succeeded)
            throw new Exception("Confirmed email failed: " + string.Join(", ", result.Errors.Select(e => e.Description)));

        return new GenericResponseDto(200, "İstifadəçi uğurla təsdiqləndi");
    }

    public async Task<GenericResponseDto> ForgotPasswordAsync(ForgotPasswordDto forgotPasswordDto)
    {
        var user = await _userManager.FindByEmailAsync(forgotPasswordDto.Email);
        if (user is null)
            throw new GenericNotFoundException("Istifadəçi tapılmadı");

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);

        var verifyUrl = $"https://localhost:7211/Users/ResetPassword?{user.Id}&{token}";

        //https://localhost:7021/Users/ResetPassword?userId&Token //numune olaraq yazilib

        string body = await this.GetEmailTemplateAsync(verifyUrl);

        MailPostDto mailPostDto = new()
        {
            ToEmail = user.Email,
            Subject = "Reset your password",
            Body = body
        };

        await EmailHelper.SendEmailAsync(mailPostDto);

        return new GenericResponseDto(200, "Mail gönderildi");
    }

    public async Task<GenericResponseDto> ResetPasswordWithResetTokenAsync(ResetPasswordDto resetPasswordDto)
    {
        var user = await _userManager.FindByIdAsync(resetPasswordDto.UserId);
        if (user is null)
            throw new GenericNotFoundException("Istifadəçi tapılmadı");

        var response = await ResetPasswordAsync(user, resetPasswordDto.ResetToken, resetPasswordDto.Password);

        return response;
    }

    public async Task<GenericResponseDto> ChangePasswordAsync(ChangePasswordDto changePasswordDto)
    {
        var user = await _userManager.FindByIdAsync(changePasswordDto.UserId);
        if (user is null)
            throw new GenericNotFoundException("Istifadəçi tapılmadı");

        var isCurrentPassword = await _userManager.CheckPasswordAsync(user, changePasswordDto.CurrentPassword);
        if (!isCurrentPassword)
            throw new GenericNotFoundException("User indiki şifrəsin yanlış daxil edib");

        var response =  await ResetPasswordAsync(user, changePasswordDto.Token, changePasswordDto.Password);

        return response;
    }

    private async Task<GenericResponseDto> ResetPasswordAsync(AppUser user, string token, string password)
    {
        var identityResult = await _userManager.ResetPasswordAsync(user, token, password);

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
