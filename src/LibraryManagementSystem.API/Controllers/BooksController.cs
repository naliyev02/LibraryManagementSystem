using LibraryManagementSystem.Business.DTOs.BookDtos;
using LibraryManagementSystem.Business.DTOs.ExtensionDtos.OrderByDtos;
using LibraryManagementSystem.Business.DTOs.ExtensionDtos.SearchDtos;
using LibraryManagementSystem.Business.DTOs.GenericPaginationAndSearchDtos;
using LibraryManagementSystem.Business.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.API.Controllers
{
    //[Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _service;

        public BooksController(IBookService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAll());
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> GetWithPaginationAndSearch(PaginationAndSearchPostDto genericPaginationAndSearchDto)
        {
            return Ok(await _service.GetWithPaginationAndSearch(genericPaginationAndSearchDto));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            return Ok(await _service.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(BookPostDto bookPostDto)
        {
            var response = await _service.CreateAsync(bookPostDto);
            return StatusCode(response.StatusCode, response.Message);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(BookPutDto bookPutDto)
        {
            var response = await _service.UpdateAsync(bookPutDto);
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
