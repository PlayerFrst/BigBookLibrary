using BigBookLibrary.Areas.Admin.ViewModels.Authors;
using BigBookLibrary.Models;
using BigBookLibrary.ViewModels.Books;

namespace BigBookLibrary.Services.Interfaces
{
    public interface IAuthorService
    {
        Task<IEnumerable<AuthorViewModel>> GetAllAsync();
        Task<AuthorFormModel?> GetByIdAsync(int id);
        Task CreateAsync(AuthorFormModel model);
        Task EditAsync(int id, AuthorFormModel model);
        Task SoftDeleteAsync(int id);
        Task<IEnumerable<BookCardViewModel>> GetBooksByAuthorAsync(int authorId);
        Task<Author?> GetAuthorEntityByIdAsync(int id);
    }
}
