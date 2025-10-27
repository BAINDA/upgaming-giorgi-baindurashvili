using System;
using BookCatalog.Application.Repositories;
using BookCatalog.Domain.Entities;
using BookCatalog.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BookCatalog.Infrastructure.Repositories
{
    public class BookRepository : IBookRepository
    {

        private readonly ApplicationDbContext _context;

        public BookRepository(ApplicationDbContext context)
        {
            this._context = context;
        }

        public async Task AddBookAsync(Book book)
        {
            
           await _context.Books.AddAsync(book);

        }

        public async Task<bool> BookExistsAsync(string bookTitle)
        {
            
            return await _context.Books.AnyAsync(b => b.Title == bookTitle);
        }

        public async Task DeleteBookAsync(int id)
        {
          var author = await _context.Books.FindAsync(id);

           _context.Books.Remove(author);
      
        }

        public async Task<IEnumerable<Book>> GetAllBooksWithAuthorAsync()
        {
            
            return await _context.Books.Include(b => b.Author).AsNoTracking().ToListAsync();
        }

        public Task<Book?> GetBookByIdAsync(int id)
        {
          return _context.Books.Include(b => b.Author).FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<IEnumerable<Book>> GetBooksByAuthorAsync(int authorId)
        {
           return await _context.Books
                     .Where(b => b.AuthorID == authorId)
                     .Include(b => b.Author)
                     .AsNoTracking()
                     .ToListAsync();

        }

        public async Task<bool> SaveChangesAsync()
        {
           return await _context.SaveChangesAsync() > 0;
        }

        public void UpdateBookAsync(Book book)
        {
             _context.Books.Update(book);


        }
    }
}

