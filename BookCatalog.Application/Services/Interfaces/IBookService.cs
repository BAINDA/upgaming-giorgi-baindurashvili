using BookCatalog.Application.Common;
using BookCatalog.Application.DTOs;
using BookCatalog.Domain.Entities;

namespace BookCatalog.Application.Services.Interfaces;

public interface IBookService
{
    Task<ApiResponse<IEnumerable<BookDto>>> GetAllBooksAsync();
    Task<ApiResponse<IEnumerable<BookDto>>> GetBooksByAuthorAsync(int authorId);
    Task<ApiResponse<BookDto?>> GetBookByIdAsync(int id);
    Task<ApiResponse<BookDto>> CreateBookAsync(CreateBookDto createBookDto);
    Task<ApiResponse<bool>> UpdateBookAsync(UpdateBookDto updateBookDto);
    Task<ApiResponse<bool>> DeleteBookAsync(int id);


}



