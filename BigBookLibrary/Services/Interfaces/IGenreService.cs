using BigBookLibrary.Models;

namespace BigBookLibrary.Services.Interfaces
{
    public interface IGenreService
    {
        Task<List<Genre>> GetAllAsync();
    }
}
