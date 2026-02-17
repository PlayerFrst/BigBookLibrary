using BigBookLibrary.Models;
using System.ComponentModel.DataAnnotations;
using static BigBookLibrary.Common.ValidationConstants;

namespace BigBookLibrary.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(BookTitleMaxLength, MinimumLength = BookTitleMinLength)]
        public string Title { get; set; } = null!;

        [StringLength(BookDescriptionMaxLength)]
        public string? Description { get; set; }

        [Required]
        [StringLength(BookIsbnLength)]
        public string ISBN { get; set; } = null!;

        public string? CoverImagePath { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public int CopiesAvailable { get; set; }

        // Foreign Keys
        [Required]
        public int AuthorId { get; set; }

        [Required]
        public int GenreId { get; set; }

        // Navigation properties
        public Author Author { get; set; } = null!;
        public Genre Genre { get; set; } = null!;

        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        public ICollection<Borrowing> Borrowings { get; set; } = new List<Borrowing>();
    }
}
