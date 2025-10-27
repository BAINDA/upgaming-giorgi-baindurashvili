using System.ComponentModel.DataAnnotations;

namespace BookCatalog.Application.DTOs
{
    public class CreateAuthorDto
    {
        [Required(ErrorMessage = "Name is required, it cannot be empty or null.")]
        public string Name { get; set; }
    }
}