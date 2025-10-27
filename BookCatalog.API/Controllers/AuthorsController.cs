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
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorsController(IAuthorService authorService) => _authorService = authorService;

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<AuthorDto>>>> GetAll()
        {
            var response = await _authorService.GetAllAuthorsAsync();
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ApiResponse<AuthorDto?>>> GetById(int id)
        {
            var response = await _authorService.GetAuthorByIdAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<AuthorDto>>> Create([FromBody] CreateAuthorDto dto)
        {
            // Model validations are handled globally via configureApiBehaviorOptions and wrapped into ApiResponse if it fails
            var response = await _authorService.CreateAuthorAsync(dto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ApiResponse<bool>>> Update(int id, [FromBody] AuthorDto dto)
        {
            // this check ensures that route and payload ids are consistent
            if (id != dto.Id)
                return BadRequest(ApiResponse<bool>.Fail("Id mismatch", 400, "Route id does not match payload id"));

            var response = await _authorService.UpdateAuthorAsync(dto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ApiResponse<bool>>> Delete(int id)
        {
            var response = await _authorService.DeleteAuthorAsync(id);
            return StatusCode(response.StatusCode, response);
        }
    }
}