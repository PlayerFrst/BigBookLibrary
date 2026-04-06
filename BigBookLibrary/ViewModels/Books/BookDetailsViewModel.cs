using BigBookLibrary.ViewModels.Reviews;

namespace BigBookLibrary.ViewModels.Books
{
    public class BookDetailsViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string AuthorName { get; set; }
        public string GenreName { get; set; }
        public string Description { get; set; }
        public string CoverImagePath { get; set; }

        public IEnumerable<ReviewViewModel> Reviews { get; set; } = new List<ReviewViewModel>();
    }
}
