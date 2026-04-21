using BigBookLibrary.ViewModels.Reviews;

namespace BigBookLibrary.ViewModels.Books
{
    public class BookDetailsViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string AuthorName { get; set; } = string.Empty;
        public string GenreName { get; set; } = string.Empty;
        public string ISBN { get; set; } = string.Empty;
        public string AuthorBiography { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string CoverImagePath { get; set; } = string.Empty;
        public int CopiesAvailable { get; set; }
        public bool AlreadyBorrowed { get; set; }
        public bool HasOverdueBooks { get; set; }

        public ReviewFormModel ReviewForm { get; set; } = new ReviewFormModel();

        public IEnumerable<ReviewViewModel> Reviews { get; set; } = new List<ReviewViewModel>();
    }
}
