using BigBookLibrary.Areas.Admin.ViewModels.Shared;
using BigBookLibrary.ViewModels.Books; 
using System.Collections.Generic;

namespace BigBookLibrary.Areas.Admin.ViewModels.Books
{
    public class BooksListViewModel
    {
        public IEnumerable<BookCardViewModel> Books { get; set; }
        public PaginationViewModel Pagination { get; set; }
    }
}
