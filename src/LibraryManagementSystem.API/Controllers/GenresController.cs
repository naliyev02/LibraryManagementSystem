using LibraryManagementSystem.Business.DTOs.GenreDtos;
using LibraryManagementSystem.Business.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenreService _service;

        public GenresController(IGenreService service)
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
        public async Task<IActionResult> CreateAsync(GenrePostDto genrePostDto)
        {
            var response = await _service.CreateAsync(genrePostDto);
            return StatusCode(response.StatusCode, response.Message);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(GenrePutDto genrePutDto)
        {
            var response = await _service.UpdateAsync(genrePutDto);
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
