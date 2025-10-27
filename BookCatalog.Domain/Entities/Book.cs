using System.ComponentModel.DataAnnotations.Schema;

namespace BookCatalog.Domain.Entities;

public class Book : BaseEntity
{
    public string Title { get; set; }
    public int PublicationYear { get; set; }
    public int AuthorID { get; set; }

    // Navigation property
    [ForeignKey(nameof(AuthorID))] 
    public Author? Author { get; set; }

}
