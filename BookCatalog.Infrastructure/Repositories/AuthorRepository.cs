using BookCatalog.Application.Repositories;
using BookCatalog.Domain.Entities;
using BookCatalog.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BookCatalog.Infrastructure.Repositories;

public class AuthorRepository : IAuthorRepository
{
 
        private readonly ApplicationDbContext _context;
        public AuthorRepository(ApplicationDbContext context)
    {
        this._context = context;
    }

        public async Task<IEnumerable<Author>> GetAllAuthorsAsync()
    {
        return await _context.Authors.AsNoTracking().ToListAsync();
    }
     

        public async Task<Author?> GetAuthorByIdAsync(int authorId)
    {
        return await _context.Authors.AsNoTracking().FirstOrDefaultAsync(author=> author.Id == authorId);
    }

        public async Task<Author> AddAuthorAsync(Author author)
        {
         var result = await _context.Authors.AddAsync(author);
         return result.Entity;
    }

        public void UpdateAuthorAsync(Author author)
        { 

           _context.Authors.Update(author);

    }

        public async Task DeleteAuthorAsync(int authorId)
        {
         
          var author = await _context.Authors.FindAsync(authorId);
          
          _context.Authors.Remove(author);

    }
        public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> AuthorExistsById(int authorId)
    {
        return await _context.Authors.AnyAsync(a => a.Id == authorId);
    }

    public async Task<bool> AuthorExistsByName(string authorName)
    {
       return await _context.Authors.AnyAsync(a => a.Name.ToLower() == authorName.ToLower());
    }
}
