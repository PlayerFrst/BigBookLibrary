using BigBookLibrary.Areas.Admin.ViewModels.Genres;
using BigBookLibrary.Data;
using BigBookLibrary.Models;
using BigBookLibrary.Services.Interfaces;
using BigBookLibrary.ViewModels.Books;
using Microsoft.EntityFrameworkCore;

public class GenreService:IGenreService
{
    private readonly ApplicationDbContext _context;

    public GenreService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<GenreViewModel>> GetAllAsync()
    {
        return await _context.Genres
            .Where(g => !g.IsDeleted)
            .Select(g => new GenreViewModel
            {
                Id = g.Id,
                Name = g.Name,
                BooksCount = g.Books.Count
            })
            .ToListAsync();
    }

    public async Task<GenreFormModel?> GetByIdAsync(int id)
    {
        var genre = await _context.Genres
       .Where(g => !g.IsDeleted && g.Id == id)
       .FirstOrDefaultAsync();

        if (genre == null)
        {
            return null;
        }

        return new GenreFormModel
        {
            Name = genre.Name
        };
    }

    public async Task CreateAsync(GenreFormModel model)
    {
        var genre = new Genre
        {
            Name = model.Name
        };

        _context.Genres.Add(genre);
        await _context.SaveChangesAsync();
    }

    public async Task EditAsync(int id, GenreFormModel model)
    {
        var genre = await _context.Genres.FindAsync(id);

        if (genre == null)
        {
            return;
        }

        genre.Name = model.Name;

        await _context.SaveChangesAsync();
    }

    public async Task SoftDeleteAsync(int id)
    {
        var genre = await _context.Genres.FirstOrDefaultAsync(g => g.Id == id);

        if (genre != null)
        {
            genre.IsDeleted = true;
            await _context.SaveChangesAsync();
        }
    }

    public async Task<List<BookCardViewModel>> GetBooksByGenreAsync(int genreId)
    {
        return await _context.Books
            .Include(b => b.Author)
            .Include(b => b.Genre)
            .Where(b => b.GenreId == genreId && !b.Genre.IsDeleted)
            .Select(b => new BookCardViewModel
            {
                Id = b.Id,
                Title = b.Title,
                AuthorName = b.Author.Name,
                GenreName = b.Genre.Name,
                CoverImagePath = b.CoverImagePath ?? "/images/no-cover.png"
            })
            .ToListAsync();
    }

}
