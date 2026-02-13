using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static BigBookLibrary.Common.ValidationConstants;

namespace BigBookLibrary.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Range(ReviewRatingMin, ReviewRatingMax)]
        public int Rating { get; set; }

        [Required]
        [StringLength(ReviewCommentMaxLength, MinimumLength = ReviewCommentMinLength)]
        public string Comment { get; set; } = null!;

        [Required]
        public DateTime CreatedOn { get; set; }

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
