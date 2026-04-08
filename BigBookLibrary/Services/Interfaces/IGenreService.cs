using BigBookLibrary.Areas.Admin.ViewModels.Genres;
using BigBookLibrary.ViewModels.Books;

namespace BigBookLibrary.Services.Interfaces
{
    public interface IGenreService
    {
        Task<IEnumerable<GenreViewModel>> GetAllAsync();
        Task<GenreFormModel?> GetByIdAsync(int id);
        Task CreateAsync(GenreFormModel model);
        Task EditAsync(int id, GenreFormModel model);
        Task SoftDeleteAsync(int id);
        Task<List<BookCardViewModel>> GetBooksByGenreAsync(int genreId);
    }
}
