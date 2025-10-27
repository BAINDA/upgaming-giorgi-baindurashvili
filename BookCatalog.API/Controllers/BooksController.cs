using BookCatalog.Application.Common;
using BookCatalog.Application.DTOs;
using BookCatalog.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookCatalog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            this._bookService = bookService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<BookDto>>>> GetAll()
        {
            var response = await _bookService.GetAllBooksAsync();
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ApiResponse<BookDto?>>> GetById(int id)
        {
            var response = await _bookService.GetBookByIdAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("by-author/{authorId:int}")]
        public async Task<ActionResult<ApiResponse<IEnumerable<BookDto>>>> GetByAuthor(int authorId)
        {
            var response = await _bookService.GetBooksByAuthorAsync(authorId);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<BookDto>>> Create([FromBody] CreateBookDto dto)
        {
            // Model validation is handled globally (ConfigureApiBehaviorOptions)
            var response = await _bookService.CreateBookAsync(dto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ApiResponse<bool>>> Update(int id, [FromBody] UpdateBookDto dto)
        {
            // keep route/payload id consistency check
            if (id != dto.Id)
                return BadRequest(ApiResponse<bool>.Fail("Id mismatch", 400, "Route id does not match payload id"));

            var response = await _bookService.UpdateBookAsync(dto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ApiResponse<bool>>> Delete(int id)
        {
            var response = await _bookService.DeleteBookAsync(id);
            return StatusCode(response.StatusCode, response);
        }
    }
}