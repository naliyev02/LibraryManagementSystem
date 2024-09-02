using LibraryManagementSystem.Business.DTOs.Identity.AuthDtos;
using LibraryManagementSystem.Business.Services.Interfaces.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            return Ok(await _authService.LoginAsync(loginDto));
        }
    }
}
