using BookCatalog.Application.Common;
using BookCatalog.Application.DTOs;
using BookCatalog.Application.Mappings;
using BookCatalog.Application.Repositories;
using BookCatalog.Application.Services.Interfaces;
using BookCatalog.Domain.Entities;

namespace BookCatalog.Application.Services.Implementations
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorService _authorService;

        public BookService(IBookRepository bookRepository, IAuthorService authorService)
        {
            this._bookRepository = bookRepository;
            this._authorService = authorService;
        }
        public async Task<ApiResponse<IEnumerable<BookDto>>> GetAllBooksAsync()
        {
            var books = await _bookRepository.GetAllBooksWithAuthorAsync();

            if (books.Any() == false)
            { 
            
               return ApiResponse<IEnumerable<BookDto>>.Fail("No books found", 404, "there is no books in database");


            }

            var bookDtos = books.ToDtos();
            return ApiResponse<IEnumerable<BookDto>>.Success(bookDtos, 200, "Books retrieved successfully");

        }

        public async Task<ApiResponse<IEnumerable<BookDto>>> GetBooksByAuthorAsync(int authorId)
        {
            
            var author = await _authorService.GetAuthorByIdAsync(authorId);

            if(author.IsSuccess is false)
            {
                return ApiResponse<IEnumerable<BookDto>>.Fail($"Author with id {authorId} not found",404,$"Cannot retrieve books for non-existing author with id {authorId}");
            }

            var books = await _bookRepository.GetBooksByAuthorAsync(authorId);

            if(books.Any() is false)
            {
                return ApiResponse<IEnumerable<BookDto>>.Fail($"No books found for author with id {authorId}",404,$"There is no books for author with id {authorId}");
            }

            var bookDtos = books.ToDtos();
            return ApiResponse<IEnumerable<BookDto>>.Success(bookDtos,200,$"Books for author with id {authorId} retrieved successfully");


        }

        public async Task<ApiResponse<BookDto?>> GetBookByIdAsync(int id)
        {
            if (id <= 0)
            {
                return ApiResponse<BookDto?>.Fail("Invalid book id", 400, $"Book id {id} is invalid");
            }

            var book = await _bookRepository.GetBookByIdAsync(id);

            if(book is null)
            {
                return ApiResponse<BookDto?>.Fail("Book not found",404,$"Book with id {id} not found");
            }
            var bookDto = book.ToDto();

            return ApiResponse<BookDto?>.Success(bookDto,200,"Book retrieved successfully");

        }

        public async Task<ApiResponse<BookDto>> CreateBookAsync(CreateBookDto createBookDto)
        {
            if (createBookDto is null)
            {
                return ApiResponse<BookDto>.Fail("CreateBookDto is null", 400, "CreateBookDto cannot be null");
            }

            var bookExists = await _bookRepository.BookExistsAsync(createBookDto.Title);

            if(bookExists is true)
            {
                return ApiResponse<BookDto>.Fail($"Book with title {createBookDto.Title} already exists",409,$"Cannot create duplicate book with title {createBookDto.Title}");
            }

            var author = await _authorService.GetAuthorByIdAsync(createBookDto.AuthorID);

            if(author.IsSuccess is false)
            {
                return ApiResponse<BookDto>.Fail($"Author with id {createBookDto.AuthorID} not found",404,$"Cannot create book for non-existing author with id {createBookDto.AuthorID}");
            }

            var book = createBookDto.ToEntity();
            await _bookRepository.AddBookAsync(book);
            if (await _bookRepository.SaveChangesAsync() is false)
            {
                return ApiResponse<BookDto>.Fail("Failed to create book", 500, "An error occurred while creating the book");
            }
            var bookDto = book.ToDto();
            return ApiResponse<BookDto>.Success(bookDto, 201, "Book created successfully");


        }

        public async Task<ApiResponse<bool>> UpdateBookAsync(UpdateBookDto updateBookDto)
        {
        
            if(updateBookDto is null)
            {
                return ApiResponse<bool>.Fail("UpdateBookDto is null",400,"UpdateBookDto cannot be null");
            }

            if(updateBookDto.Id <= 0)
            {
                return ApiResponse<bool>.Fail("Invalid book id", 400, $"Book id {updateBookDto.Id} is invalid");
            }

           var book = await _bookRepository.GetBookByIdAsync(updateBookDto.Id);

           if(book is null)
           {
                return ApiResponse<bool>.Fail("Book not found",404,$"Book with id {updateBookDto.Id} not found");
            }

              book.AuthorID = updateBookDto.AuthorID;

             var author = await _authorService.GetAuthorByIdAsync(book.AuthorID);

               if(author.IsSuccess is false)
                {
                return ApiResponse<bool>.Fail($"Author with id {book.AuthorID} not found",404,$"Cannot update book to non-existing author with id {book.AuthorID}");
               
                }

               book.Title = updateBookDto.Title;
               book.PublicationYear = updateBookDto.PublicationYear;
              var updateResult = await _bookRepository.UpdateBookAsync(book);

            if(updateResult is false || await _bookRepository.SaveChangesAsync() is false)
            {
                return ApiResponse<bool>.Fail("Failed to update book",500,"An error occurred while updating the book");
            }

            return ApiResponse<bool>.Success(true,200,"Book updated successfully");

        }
        public async Task<ApiResponse<bool>> DeleteBookAsync(int id)
        {
            if (id <= 0)
            {
                return ApiResponse<bool>.Fail("Invalid book id", 400, $"Book id {id} is invalid");
            }

            var book = await _bookRepository.GetBookByIdAsync(id);

            if (book is null)
            {
                return ApiResponse<bool>.Fail("Book not found", 404, $"Book with id {id} not found");
            }

            var deleteResult = await _bookRepository.DeleteBookAsync(id);
            if (deleteResult is false || await _bookRepository.SaveChangesAsync() is false)
            {
                return ApiResponse<bool>.Fail("Failed to delete book", 500, "An error occurred while deleting the book");
            }
            return ApiResponse<bool>.Success(true, 200, "Book deleted successfully");
        }
    }
}
