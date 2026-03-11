namespace BigBookLibrary.Areas.Admin.ViewModels.Genres
{
    public class GenreViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int BooksCount { get; set; }
    }
}
