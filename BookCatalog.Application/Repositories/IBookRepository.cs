

using BookCatalog.Domain.Entities;

namespace BookCatalog.Application.Repositories;

public interface IBookRepository
{
    Task<IEnumerable<Book>> GetAllBooksWithAuthorAsync();
    Task<Book?> GetBookByIdAsync(int id);
    Task<IEnumerable<Book>> GetBooksByAuthorAsync(int authorId);
    Task<bool> BookExistsAsync(string bookTitle);
    Task<Book> AddBookAsync(Book book);
    Task<bool> UpdateBookAsync(Book book);
    Task<bool> DeleteBookAsync(int id);
    Task<bool> SaveChangesAsync();

}
