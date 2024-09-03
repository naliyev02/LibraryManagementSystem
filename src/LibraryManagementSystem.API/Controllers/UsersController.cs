using LibraryManagementSystem.Business.DTOs.Identity.AuthDtos;
using LibraryManagementSystem.Business.DTOs.UserDtos;
using LibraryManagementSystem.Business.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;

        public UsersController(IUserService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            return Ok(await _service.GetUser());
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterDto userRegisterDto)
        {
            return Ok(await _service.RegisterUserAsync(userRegisterDto));
        }

        [HttpPost("AddRole")]
        public async Task<IActionResult> AddRole(AddRoleToUserDto addRoleToUserDto)
        {
            return Ok(await _service.AddRoleToUserAsync(addRoleToUserDto));
        }
    }
}
