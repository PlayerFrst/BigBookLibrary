using System.ComponentModel.DataAnnotations;
using static BigBookLibrary.Common.ValidationConstants;

namespace BigBookLibrary.Models
{
    public class Genre
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(GenreNameMaxLength, MinimumLength = GenreNameMinLength)]
        public string Name { get; set; } = null!;

        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
