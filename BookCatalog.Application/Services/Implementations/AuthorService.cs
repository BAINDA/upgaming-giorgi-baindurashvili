using System.Collections.Immutable;
using BookCatalog.Application.Common;
using BookCatalog.Application.DTOs;
using BookCatalog.Application.Mappings;
using BookCatalog.Application.Repositories;
using BookCatalog.Application.Services.Interfaces;

namespace BookCatalog.Application.Services.Implementations;

public class AuthorService : IAuthorService
{
    private readonly IAuthorRepository _authorRepository;

    public AuthorService(IAuthorRepository authorRepository)
    {
        this._authorRepository = authorRepository;
    }
    public async Task<ApiResponse<IEnumerable<AuthorDto>>> GetAllAuthorsAsync()
    {
        
        var authors =  await _authorRepository.GetAllAuthorsAsync();

        if(authors.Any() is false)
        {
              return ApiResponse<IEnumerable<AuthorDto>>.Fail("No authors found",404,"there is no authors in database");
        }

        var authorsDtos = authors.ToDtos();

        return ApiResponse<IEnumerable<AuthorDto>>.Success(authorsDtos,200,"Authors retrieved successfully");

    }

    public async Task<ApiResponse<AuthorDto?>> GetAuthorByIdAsync(int authorId)
    {

        if (authorId <= 0)
        {
            return ApiResponse<AuthorDto?>.Fail("Invalid author id",400,$"Author id {authorId} is invalid");
        }

        var author = await _authorRepository.GetAuthorByIdAsync(authorId);

        if(author is null)
        {
            return ApiResponse<AuthorDto?>.Fail("Author not found", 404, $"Author with id {authorId} not found");
        }

        var authorDto = author.ToDto();

        return (ApiResponse<AuthorDto?>.Success(authorDto,200,"Author retrieved successfully"));

    }


    public async Task<ApiResponse<AuthorDto>> CreateAuthorAsync(CreateAuthorDto createAuthorDto)
    {
     
         if (createAuthorDto is null)
        {
            return ApiResponse<AuthorDto>.Fail("Author data is null",400,"Cannot create author with null data");
        }

        var authorExists = await _authorRepository.AuthorExistsByName(createAuthorDto.Name);

        if(authorExists is true)
        {
            return ApiResponse<AuthorDto>.Fail($"Author with name {createAuthorDto.Name} already exists",409,$"Cannot create duplicate author with name {createAuthorDto.Name}");
        }

        var author = createAuthorDto.ToEntity();
        await _authorRepository.AddAuthorAsync(author);


        if (await _authorRepository.SaveChangesAsync() == false)
        {
            return ApiResponse<AuthorDto>.Fail("Failed to create author",500,"An error occurred while creating the author");
        }

        var authorDto = author.ToDto();
        return ApiResponse<AuthorDto>.Success(authorDto,201,"Author created successfully");

    }
    public async Task<ApiResponse<bool>> UpdateAuthorAsync(AuthorDto authorDto)
    {
        if (authorDto.Id <= 0)
        {
            return ApiResponse<bool>.Fail("Invalid author id", 400, $"Author id {authorDto.Id} is invalid");
        }

        var authorToUpdate = await _authorRepository.GetAuthorByIdAsync(authorDto.Id);

        if (authorToUpdate is null)
        {
            {
                return ApiResponse<bool>.Fail("Author not found", 404, $"Author with id {authorDto.Id} not found");

            }

        }

        authorToUpdate.Name = authorDto.Name;
       _authorRepository.UpdateAuthorAsync(authorToUpdate);

        if(await _authorRepository.SaveChangesAsync() is false)
        {
            return ApiResponse<bool>.Fail("Failed to update author", 500, "An error occurred while updating the author");
        }
       
        return ApiResponse<bool>.Success(true, 200, "Author updated successfully");

    }


    public async Task<ApiResponse<bool>> DeleteAuthorAsync(int authorId)
    {
       if(authorId <= 0)
        {
            return ApiResponse<bool>.Fail("Invalid author id", 400, $"Author id {authorId} is invalid");
        }

       var authorExists = await _authorRepository.AuthorExistsById(authorId);

       if(authorExists is false)
        {
           return ApiResponse<bool>.Fail("Author not found", 404, $"Author with id {authorId} not found");
        }

         await _authorRepository.DeleteAuthorAsync(authorId);

         if(await _authorRepository.SaveChangesAsync() is false) { 
          
                return ApiResponse<bool>.Fail("Failed to delete author", 500, "An error occurred while deleting the author");
        }
            return ApiResponse<bool>.Success(true, 200, "Author deleted successfully");


    }

}
