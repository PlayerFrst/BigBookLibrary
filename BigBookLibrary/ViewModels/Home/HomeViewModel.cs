using BigBookLibrary.ViewModels.Books;

namespace BigBookLibrary.ViewModels.Home
{
    public class HomeViewModel
    {
        public IEnumerable<BookCardViewModel> PopularBooks { get; set; } = new List<BookCardViewModel>();
    }
}
