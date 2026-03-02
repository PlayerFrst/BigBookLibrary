namespace BigBookLibrary.Areas.Admin.ViewModels.Books
{
    public class BookDetailsViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ISBN { get; set; }
        public int Year { get; set; }
        public int CopiesAvailable { get; set; }
        public string CoverImagePath { get; set; }
        public string AuthorName { get; set; }
        public string AuthorBiography { get; set; } 
        public string GenreName { get; set; } 
    }
}
