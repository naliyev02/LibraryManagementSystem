using LibraryManagementSystem.Business.DTOs.CategoryDtos;
using LibraryManagementSystem.Business.DTOs.LanguageDtos;
using LibraryManagementSystem.Business.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _service;

        public CategoriesController(ICategoryService service)
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
        public async Task<IActionResult> CreateAsync(CategoryPostDto categoryPostDto)
        {
            var response = await _service.CreateAsync(categoryPostDto);
            return StatusCode(response.StatusCode, response.Message);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(CategoryPutDto categoryPutDto)
        {
            var response = await _service.UpdateAsync(categoryPutDto);
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
