using LibraryManagementSystem.Business.DTOs.Identity.AuthDtos;
using LibraryManagementSystem.Business.DTOs.UserDtos;
using LibraryManagementSystem.Business.Services.Interfaces;
using LibraryManagementSystem.Business.Services.Interfaces.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly IAuthService _authService;

        public UsersController(IUserService service, IAuthService authService)
        {
            _service = service;
            _authService = authService;
        }

        [HttpGet("GetUserInfo")]
        public async Task<IActionResult> GetUserInfo()
        {
            return Ok(await _service.GetUserInfo());
        }

        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            return Ok(await _service.GetAllUsers());
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterDto userRegisterDto)
        {
            return Ok(await _service.RegisterUserAsync(userRegisterDto));
        }

        //[Authorize(Roles = "Admin")]
        [HttpPost("AddRole")]
        public async Task<IActionResult> AddRole(AddRoleToUserDto addRoleToUserDto)
        {
            return Ok(await _service.AddRoleToUserAsync(addRoleToUserDto));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> ResetPassword(ChangePasswordDto changePasswordDto)
        {

            return Ok(await _authService.ResetPasswordAsync(changePasswordDto));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDto forgotPasswordDto)
        {

            return Ok(await _authService.ForgotPasswordAsync(forgotPasswordDto));
        }
    }
}
