using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookCatalog.Application.Common;
using BookCatalog.Application.DTOs;
using BookCatalog.Domain.Entities;

namespace BookCatalog.Application.Services.Interfaces
{
    public interface IAuthorService
    {
        Task<ApiResponse<AuthorDto?>> GetAuthorByIdAsync(int authorId);
        Task<ApiResponse<AuthorDto>> CreateAuthorAsync(CreateAuthorDto createAuthorDto);
        Task<ApiResponse<bool>> UpdateAuthorAsync(AuthorDto authorDto);
        Task<ApiResponse<bool>> DeleteAuthorAsync(int authorId);
        Task<ApiResponse<IEnumerable<AuthorDto>>> GetAllAuthorsAsync();
    }
}
