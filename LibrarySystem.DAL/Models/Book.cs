namespace LibrarySystem.DAL.Models
{
    public class Book : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string ISBN { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int NumberOfPages { get; set; }
        public string Description { get; set; } = string.Empty;
        public string? ImageName { get; set; }
        public bool IsAvailable { get; set; }

        public int AuthorId { get; set; }
        public Author Author { get; set; } = null!;
    }
}