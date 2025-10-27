using BookCatalog.Application.DTOs;
using BookCatalog.Domain.Entities;

namespace BookCatalog.Application.Mappings
{
    public static class AuthorMappingExtensions
    {
        public static AuthorDto ToDto(this Author author)
        {
            if (author is null) throw new ArgumentNullException(nameof(author));

            return new AuthorDto
            {
                Id = author.Id,
                Name = author.Name ?? string.Empty
            };
        }

        public static IEnumerable<AuthorDto> ToDtos(this IEnumerable<Author>? authors)
        {
            if (authors is null) return Enumerable.Empty<AuthorDto>();
            return authors.Select(a => a.ToDto());
        }

        public static Author ToEntity(this CreateAuthorDto dto)
        {
            if (dto is null) throw new ArgumentNullException(nameof(dto));

            return new Author
            {
                Name = dto.Name
            };
        }

        public static Author ToEntity(this AuthorDto dto)
        {
            if (dto is null) throw new ArgumentNullException(nameof(dto));

            return new Author
            {
                Id = dto.Id,
                Name = dto.Name
            };
        }

        public static IEnumerable<Author> ToEntities(this IEnumerable<AuthorDto>? dtos)
        {
            if (dtos is null) return Enumerable.Empty<Author>();
            return dtos.Select(d => d.ToEntity());
        }

    
    }
}