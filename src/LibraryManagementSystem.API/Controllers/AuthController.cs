using LibraryManagementSystem.Business.DTOs.Identity.AuthDtos;
using LibraryManagementSystem.Business.DTOs.Identity.TokenDtos;
using LibraryManagementSystem.Business.Exceptions;
using LibraryManagementSystem.Business.Services.Interfaces.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;
        private readonly ITokenService _tokenService;

        public AuthController(IAuthService service, ITokenService tokenService)
        {
            _service = service;
            _tokenService = tokenService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            return Ok(await _service.LoginAsync(loginDto));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> RefreshToken(TokenDto tokenDto)
        {
            return Ok(await _tokenService.RefreshToken(tokenDto));
        }
    }
}
