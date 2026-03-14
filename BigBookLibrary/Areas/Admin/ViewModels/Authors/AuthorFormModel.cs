using System.ComponentModel.DataAnnotations;
using static BigBookLibrary.Common.ValidationConstants;

namespace BigBookLibrary.Areas.Admin.ViewModels.Authors
{
    public class AuthorFormModel
    {
        [Required]
        [StringLength(AuthorNameMaxLength, MinimumLength = AuthorNameMinLength)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(AuthorBiographyMaxLength)]
        public string Biography { get; set; } = string.Empty;
    }
}
