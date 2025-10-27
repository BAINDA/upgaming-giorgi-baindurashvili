

namespace BookCatalog.Application.DTOs
{
    public class BookFilterParams
    {
        public int? PublicationYear { get; set; }
        public string? TitleKeyword { get; set; }
        public string? AuthorName { get; set; }
        public string? SortBy { get; set; }
    }
}
