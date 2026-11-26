using System.ComponentModel.DataAnnotations;

namespace LibrarySystem.PL.ViewModels
{
    public class BookViewModel
    {
        public int Id { get; set; }

        [StringLength(maximumLength: 100, MinimumLength = 3)]
        public string Title { get; set; } = string.Empty;

        [StringLength(20, MinimumLength = 5)]
        public string ISBN { get; set; } = string.Empty;

        [DataType(DataType.Currency)]
        [Range(0.01, 100000)]
        public decimal Price { get; set; }

        [Range(1, 5000)]
        public int NumberOfPages { get; set; }

        public string Description { get; set; } = string.Empty;

        public IFormFile? Image { get; set; }
        public string? ImageName { get; set; }

        public bool IsAvailable { get; set; }

        public Author? Author { get; set; }
        public int? AuthorId { get; set; }
    }
}