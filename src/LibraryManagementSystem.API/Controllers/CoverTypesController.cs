using LibraryManagementSystem.Business.DTOs.CoverTypeDtos;
using LibraryManagementSystem.Business.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoverTypesController : ControllerBase
    {
        private readonly ICoverTypeService _service;

        public CoverTypesController(ICoverTypeService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            return Ok(await _service.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CoverTypePostDto coverTypePostDto)
        {
            var response = await _service.CreateAsync(coverTypePostDto);
            return StatusCode(response.StatusCode, response.Message);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(CoverTypePutDto coverTypePutDto)
        {
            var response = await _service.UpdateAsync(coverTypePutDto);
            return StatusCode(response.StatusCode, response.Message);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var response = await _service.DeleteAsync(id);
            return StatusCode(response.StatusCode, response.Message);
        }
    }
}
