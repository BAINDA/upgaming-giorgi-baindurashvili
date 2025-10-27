using BookCatalog.Application.DTOs;
using BookCatalog.Application.Repositories;
using BookCatalog.Domain.Entities;
using BookCatalog.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

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
            
            return await _context.Books.AnyAsync(b => b.Title.ToLower() == bookTitle.ToLower());
        }

        public async Task DeleteBookAsync(int id)
        {
          var author = await _context.Books.FindAsync(id);

           _context.Books.Remove(author);
      
        }

        public async Task<IEnumerable<Book>> GetBooksAsync(BookFilterParams filterParams)
        {

            var query = _context.Books
                  .Include(b => b.Author)
                  .AsNoTracking()
                  .AsQueryable();

            if(string.IsNullOrWhiteSpace(filterParams.AuthorName) is false)
            {
                var searchName = filterParams.AuthorName.Trim().ToLower();
                query = query.Where(b => b.Author!.Name.ToLower().Contains(searchName));
            }

            if (filterParams.PublicationYear.HasValue && filterParams.PublicationYear.Value > 0)
            {
                query = query.Where(b => b.PublicationYear == filterParams.PublicationYear.Value);
            }

            if (string.IsNullOrWhiteSpace(filterParams.TitleKeyword) is false)
            {
                var searchKeyword = filterParams.TitleKeyword.Trim().ToLower();
                query = query.Where(b => b.Title.ToLower().Contains(searchKeyword));
            }

            if(string.IsNullOrWhiteSpace(filterParams.SortBy) is false)
            {
                     switch(filterParams.SortBy.ToLower())
                {
                    case "title_asc":
                        query = query.OrderBy(b => b.Title);
                        break;
                    case "title_desc":
                        query = query.OrderByDescending(b => b.Title);
                        break;
                    case "year_asc":
                        query = query.OrderBy(b => b.PublicationYear);
                        break;
                    case "year_desc":
                        query = query.OrderByDescending(b => b.PublicationYear);
                        break;
                    case "author_asc":
                        query = query.OrderBy(b => b.Author!.Name);
                        break;
                    case "author_desc":
                        query = query.OrderByDescending(b => b.Author!.Name);
                        break;
                    default:
                        break;
                }
            }

            return await query.ToListAsync();

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

