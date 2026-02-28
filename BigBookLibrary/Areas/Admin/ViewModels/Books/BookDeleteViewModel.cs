namespace BigBookLibrary.Areas.Admin.ViewModels.Books
{
    public class BookDeleteViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public int Year { get; set; }
        public string AuthorName { get; set; } = null!;
    }
}
