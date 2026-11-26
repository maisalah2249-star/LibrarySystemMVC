using System.ComponentModel.DataAnnotations;

namespace LibrarySystem.DAL.Models
{
    public class Author : BaseEntity
    {
        [Range(0, 1000)]
        public int Code { get; set; }

        [Required(ErrorMessage = "Name is Required!")]
        public string Name { get; set; } = string.Empty;

        public DateTime DateOfBirth { get; set; }

        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}