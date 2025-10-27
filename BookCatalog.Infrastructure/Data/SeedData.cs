
using BookCatalog.Domain.Entities;
using BookCatalog.Infrastructure.Data;

public static class SeedData
{
    public static void Initialize(ApplicationDbContext context)
    {
        
        if (context.Authors.Any() || context.Books.Any() )
        {
            return;
        }

        var authors = new Author[]
        {
           new Author { Id = 1, Name = "Robert C. Martin" },
           new Author { Id = 2, Name = "Jeffrey Richter"  }
        };

        context.Authors.AddRange(authors);
        context.SaveChanges();

        var books = new Book[]
        {
        new Book { Id = 1, Title = "Clean Code", AuthorID = 1, PublicationYear = 2008 },
        new Book { Id = 2, Title = "CLR via C#", AuthorID = 2, PublicationYear = 2012 },
        new Book { Id = 3, Title = "The Clean Coder", AuthorID = 1, PublicationYear = 2011 }
        };

        context.Books.AddRange(books);
        context.SaveChanges();
    }
}