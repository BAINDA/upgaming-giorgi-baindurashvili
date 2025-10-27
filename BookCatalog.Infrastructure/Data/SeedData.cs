using BookCatalog.Domain.Entities;
using BookCatalog.Infrastructure.Data;

public static class SeedData
{
    public static void Initialize(ApplicationDbContext context)
    {
        // Check if data already exists
        if (context.Authors.Any() || context.Books.Any())
        {
            return;
        }

      
        var authors = new Author[]
        {
        
            new Author { Id = 1, Name = "Robert C. Martin" },
            new Author { Id = 2, Name = "Jeffrey Richter" },
            new Author { Id = 3, Name = "Shota Rustaveli" },   
            new Author { Id = 4, Name = "Nodar Dumbadze" },    
            new Author { Id = 5, Name = "Mikheil Javakhishvili" },
            new Author { Id = 6, Name = "Otar Chiladze" }   
        };

        context.Authors.AddRange(authors);
        context.SaveChanges();


        var books = new Book[]
        {
        
            new Book { Id = 1, Title = "Clean Code", AuthorID = 1, PublicationYear = 2008 },
            new Book { Id = 2, Title = "CLR via C#", AuthorID = 2, PublicationYear = 2012 },
            new Book { Id = 3, Title = "The Clean Coder", AuthorID = 1, PublicationYear = 2011 },
            new Book { Id = 4, Title = "The Knight in the Panther's Skin", AuthorID = 3, PublicationYear = 1195 }, 
            new Book { Id = 5, Title = "Me, Granny, Iliko and Ilarion", AuthorID = 4, PublicationYear = 1960 },
            new Book { Id = 6, Title = "I See the Sun", AuthorID = 4, PublicationYear = 1962 },
            new Book { Id = 7, Title = "Jaqo's Dispossessed", AuthorID = 5, PublicationYear = 1925 },
            new Book { Id = 8, Title = "Kvachi Kvachantiradze", AuthorID = 5, PublicationYear = 1924 },
            new Book { Id = 9, Title = "A Man Was Going Down the Road", AuthorID = 6, PublicationYear = 1973 }
        };

        context.Books.AddRange(books);
        context.SaveChanges();
    }
}