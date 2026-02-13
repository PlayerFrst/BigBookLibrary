using System.ComponentModel.DataAnnotations;
using static BigBookLibrary.Common.ValidationConstants;

namespace BigBookLibrary.Models
{
    public class Author
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(AuthorNameMaxLength, MinimumLength = AuthorNameMinLength)]
        public string Name { get; set; } = null!;

        [StringLength(AuthorBiographyMaxLength)]
        public string? Biography { get; set; }

        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
