

using BookCatalog.Application.DTOs;
using BookCatalog.Domain.Entities;

namespace BookCatalog.Application.Repositories;

public interface IBookRepository
{
    Task<IEnumerable<Book>> GetBooksAsync(BookFilterParams filterParams);
    Task<Book?> GetBookByIdAsync(int id);
    Task<IEnumerable<Book>> GetBooksByAuthorAsync(int authorId);
    Task<bool> BookExistsAsync(string bookTitle);
    Task AddBookAsync(Book book);
    void UpdateBookAsync(Book book);
    Task DeleteBookAsync(int id);
    Task<bool> SaveChangesAsync();

}
