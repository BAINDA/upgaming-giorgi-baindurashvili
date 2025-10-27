using BookCatalog.Domain.Entities;

namespace BookCatalog.Application.Repositories
{
    public interface IAuthorRepository
    {

        Task<IEnumerable<Author>> GetAllAuthorsAsync();
        Task<Author?> GetAuthorByIdAsync(int authorId);
        Task<Author> AddAuthorAsync(Author author);
        Task<bool> UpdateAuthorAsync(Author author);
        Task<bool> DeleteAuthorAsync(int authorId);
        Task<bool> AuthorExistsAsync(int authorId);
        Task<bool> SaveChangesAsync();


    }
}
