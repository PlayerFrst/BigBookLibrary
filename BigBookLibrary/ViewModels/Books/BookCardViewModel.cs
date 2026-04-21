namespace BigBookLibrary.ViewModels.Books
{
    public class BookCardViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string AuthorName { get; set; } = null!;
        public string GenreName { get; set; } = null!;
        public string CoverImagePath { get; set; } = null!;
        public string DetailsUrl { get; set; } = null!;
        public string AuthorBiography { get; set; } = null!;

    }

}
