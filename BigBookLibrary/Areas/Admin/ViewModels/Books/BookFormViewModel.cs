using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using static BigBookLibrary.Common.ValidationConstants;

namespace BigBookLibrary.Areas.Admin.ViewModels.Books
{
    public class BookFormViewModel
    {

        public int? Id { get; set; }

        [Required]
        [StringLength(BookTitleMaxLength, MinimumLength = BookTitleMinLength)]
        public string Title { get; set; }

        [StringLength(BookDescriptionMaxLength)]
        public string Description { get; set; }

        [Required]
        [StringLength(BookIsbnLength)]
        public string ISBN { get; set; }

        public string CoverImagePath { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public int CopiesAvailable { get; set; }

        [Required]
        public int AuthorId { get; set; }
        [Required]
        public int GenreId { get; set; }

        public IEnumerable<SelectListItem> Authors { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem> Genres { get; set; } = new List<SelectListItem>();

        public string Heading => Id == null ? "Create Book" : "Edit Book";
    }
}
