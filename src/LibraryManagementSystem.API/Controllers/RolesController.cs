using LibraryManagementSystem.Business.DTOs.RoleDtos;
using LibraryManagementSystem.Business.Services.Implementations;
using LibraryManagementSystem.Business.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _service;
        public RolesController(IRoleService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Post(RolePostDto rolePostDto)
        {
            await _service.CreateRoleAsync(rolePostDto);
            return StatusCode(StatusCodes.Status201Created);
        }
    }
}
