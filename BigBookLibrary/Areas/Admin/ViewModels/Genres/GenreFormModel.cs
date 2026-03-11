using System.ComponentModel.DataAnnotations;
using static BigBookLibrary.Common.ValidationConstants;

namespace BigBookLibrary.Areas.Admin.ViewModels.Genres
{
    public class GenreFormModel
    {
        [Required(ErrorMessage = "The 'Name' field is required.")]
        [StringLength(
           GenreNameMaxLength,
           MinimumLength = GenreNameMinLength,
           ErrorMessage = "The name must be between {2} and {1} characters long."
       )]
        public string Name { get; set; } = string.Empty;
    }
}
