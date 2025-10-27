using BookCatalog.Domain.Entities;

namespace BookCatalog.Application.Repositories
{
    public interface IAuthorRepository
    {

        Task<IEnumerable<Author>> GetAllAuthorsAsync();
        Task<Author?> GetAuthorByIdAsync(int authorId);
        Task<Author> AddAuthorAsync(Author author);
        void UpdateAuthorAsync(Author author);
        Task DeleteAuthorAsync(int authorId);
        Task<bool> AuthorExistsById(int authorId);
        Task<bool> AuthorExistsByName(string authorName);
        Task<bool> SaveChangesAsync();


    }
}
