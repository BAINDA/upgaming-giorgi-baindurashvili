using System;
using System.Collections.Generic;
using System.Linq;
using BookCatalog.Application.DTOs;
using BookCatalog.Domain.Entities;

namespace BookCatalog.Application.Mappings
{
    public static class BookMappingExtensions
    {
        public static BookDto ToDto(this Book book)
        {
            if (book is null) throw new ArgumentNullException(nameof(book));

            return new BookDto
            {
                ID = book.Id,
                Title = book.Title ?? string.Empty,
                AuthorName = book.Author?.Name ?? string.Empty,
                PublicationYear = book.PublicationYear
            };
        }

        public static IEnumerable<BookDto> ToDtos(this IEnumerable<Book>? books)
        {
            if (books is null) return Enumerable.Empty<BookDto>();
            return books.Select(b => b.ToDto());
        }

        public static Book ToEntity(this CreateBookDto dto)
        {
            if (dto is null) throw new ArgumentNullException(nameof(dto));

            return new Book
            {
                Title = dto.Title,
                AuthorID = dto.AuthorID,
                PublicationYear = dto.PublicationYear
            };
        }

        public static IEnumerable<Book> ToEntities(this IEnumerable<CreateBookDto>? dtos)
        {
            if (dtos is null) return Enumerable.Empty<Book>();
            return dtos.Select(d => d.ToEntity());
        }

   
        public static Book ToEntity(this BookDto dto, int authorId)
        {
            if (dto is null) throw new ArgumentNullException(nameof(dto));

            return new Book
            {
                Id = dto.ID,
                Title = dto.Title,
                AuthorID = authorId,
                PublicationYear = dto.PublicationYear
            };
        }
        public static IEnumerable<Book> ToEntities(this IEnumerable<BookDto>? dtos, Func<BookDto, int> authorIdSelector)
        {
            if (dtos is null) return Enumerable.Empty<Book>();
            if (authorIdSelector is null) throw new ArgumentNullException(nameof(authorIdSelector));

            return dtos.Select(dto => dto.ToEntity(authorIdSelector(dto)));
        }

    }
}