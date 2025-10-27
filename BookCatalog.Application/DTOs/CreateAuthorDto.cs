

using System.ComponentModel.DataAnnotations;

namespace BookCatalog.Application.DTOs
{
    public class CreateAuthorDto
    {
        private string? _name;

        [Required(ErrorMessage = "Name is required, it cannot be empty or null.")]
        public string Name
        {
            get => _name ?? string.Empty;
     
            set => _name = value?.Trim();
        }
    }
}