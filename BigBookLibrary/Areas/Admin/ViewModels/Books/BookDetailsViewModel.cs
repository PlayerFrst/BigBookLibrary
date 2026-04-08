namespace BigBookLibrary.Areas.Admin.ViewModels.Books
{
    public class BookDetailsViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ISBN { get; set; } = string.Empty;
        public int Year { get; set; }
        public int CopiesAvailable { get; set; }
        public string CoverImagePath { get; set; } = string.Empty;
        public string AuthorName { get; set; } = string.Empty;
        public string AuthorBiography { get; set; } = string.Empty;
        public string GenreName { get; set; } = string.Empty;
    }
}
