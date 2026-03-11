using BigBookLibrary.Areas.Admin.ViewModels.Genres;

namespace BigBookLibrary.Services.Interfaces
{
    public interface IGenreService
    {
        Task<IEnumerable<GenreViewModel>> GetAllAsync();
        Task<GenreFormModel?> GetByIdAsync(int id);
        Task CreateAsync(GenreFormModel model);
        Task EditAsync(int id, GenreFormModel model);
        Task SoftDeleteAsync(int id);
    }
}
