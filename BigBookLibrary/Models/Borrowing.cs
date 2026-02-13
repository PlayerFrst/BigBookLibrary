using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BigBookLibrary.Models
{
    public class Borrowing
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime BorrowedOn { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        public DateTime? ReturnedOn { get; set; }

        // Foreign Keys
        [Required]
        public int BookId { get; set; }

        public string UserId { get; set; } = null!;

        [ForeignKey(nameof(UserId))]
        public IdentityUser User { get; set; } = null!;

        // Navigation
        public Book Book { get; set; } = null!;
    }
}
