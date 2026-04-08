using BigBookLibrary.Models;
using BigBookLibrary.ViewModels.Books;

namespace BigBookLibrary.Services.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<BookCardViewModel>> GetAllBooksAsync();
        Task<Book?> GetBookByIdAsync(int id);
        Task CreateBookAsync(Book book);
        Task UpdateBookAsync(Book book);
        Task DeleteBookAsync(Book book);
        Task<BookDetailsViewModel?> GetBookDetailsAsync(int id);
        Task<List<Book>> SearchBooksAsync(string query);

    }
}
