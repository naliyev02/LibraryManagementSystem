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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllRolesAsync());
        }


        [HttpPost]
        public async Task<IActionResult> Create(RolePostDto rolePostDto)
        {
            await _service.CreateRoleAsync(rolePostDto);
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string name)
        {
            await _service.DeleteRoleAsync(name);
            return StatusCode(StatusCodes.Status201Created);
        }
    }
}
