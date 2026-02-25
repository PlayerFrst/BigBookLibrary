using Microsoft.AspNetCore.Mvc.Rendering;

namespace BigBookLibrary.Areas.Admin.ViewModels.Books
{
    public class BookFormViewModel
    {

        public int? Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ISBN { get; set; }
        public string CoverImagePath { get; set; }
        public int Year { get; set; }
        public int CopiesAvailable { get; set; }

        public int AuthorId { get; set; }
        public int GenreId { get; set; }

        public IEnumerable<SelectListItem> Authors { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem> Genres { get; set; } = new List<SelectListItem>();

        public string Heading => Id == null ? "Create Book" : "Edit Book";
    }
}
