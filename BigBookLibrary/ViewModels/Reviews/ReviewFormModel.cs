using System.ComponentModel.DataAnnotations;
using static BigBookLibrary.Common.ValidationConstants;

namespace BigBookLibrary.ViewModels.Reviews
{
    public class ReviewFormModel
    {
        public int Id { get; set; } 

        [Range(ReviewRatingMin, ReviewRatingMax)]
        public int Rating { get; set; }

        [Required]
        [StringLength(ReviewCommentMaxLength, MinimumLength = ReviewCommentMinLength)]
        public string Comment { get; set; } = null!;
    }
}
