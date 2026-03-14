using BigBookLibrary.Areas.Admin.ViewModels.Authors;

namespace BigBookLibrary.Services.Interfaces
{
    public interface IAuthorService
    {
        Task<IEnumerable<AuthorViewModel>> GetAllAsync();
        Task<AuthorFormModel?> GetByIdAsync(int id);
        Task CreateAsync(AuthorFormModel model);
        Task EditAsync(int id, AuthorFormModel model);
        Task SoftDeleteAsync(int id);
    }
}
