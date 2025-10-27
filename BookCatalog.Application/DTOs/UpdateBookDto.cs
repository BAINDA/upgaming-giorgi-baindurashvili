using System.ComponentModel.DataAnnotations;

namespace BookCatalog.Application.DTOs
{
    public class UpdateBookDto
    {

        [Required(ErrorMessage = "Id is required.")]
        public int Id { get; set; }
        public string Title { get; set; }

        [Required(ErrorMessage = "AuthorID is required.")]
        public int AuthorID { get; set; }

        [Range(1000, 2025, ErrorMessage = "Publication year must be between 1000 and 2025.")]
        public int PublicationYear { get; set; }
    }
}
