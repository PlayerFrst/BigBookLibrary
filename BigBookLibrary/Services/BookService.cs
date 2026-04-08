using BigBookLibrary.Data;
using BigBookLibrary.Models;
using BigBookLibrary.Services.Interfaces;
using BigBookLibrary.ViewModels.Books;
using BigBookLibrary.ViewModels.Reviews;
using Microsoft.EntityFrameworkCore;

namespace BigBookLibrary.Services
{
    public class BookService : IBookService
    {
        private readonly ApplicationDbContext _context;

        public BookService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BookCardViewModel>> GetAllBooksAsync()
        {
            return await _context.Books
                .Where(b => !b.IsDeleted)
                .Include(b => b.Author)
                .Include(b => b.Genre)
                .Select(b => new BookCardViewModel
                {
                    Id = b.Id,
                    Title = b.Title,
                    AuthorName = b.Author.Name,
                    GenreName = b.Genre.Name,
                    CoverImagePath = b.CoverImagePath ?? "/images/no-cover.png",
                })
                .ToListAsync();
        }

        public async Task<Book?> GetBookByIdAsync(int id)
        {
            return await _context.Books
                .Where(b => !b.IsDeleted)
                .Include(b => b.Author)
                .Include(b => b.Genre)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task CreateBookAsync(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBookAsync(Book book)
        {
            _context.Books.Update(book);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBookAsync(Book book)
        {
            book.IsDeleted = true;
            await _context.SaveChangesAsync();
        }

        public async Task<BookDetailsViewModel?> GetBookDetailsAsync(int id)
        {
            return await _context.Books
        .Where(b => !b.IsDeleted && b.Id == id)
        .Select(b => new BookDetailsViewModel
        {
            Id = b.Id,
            Title = b.Title,
            Description = b.Description ?? "No description available",
            CoverImagePath = b.CoverImagePath ?? "/images/no-cover.png",
            AuthorName = b.Author.Name,
            GenreName = b.Genre.Name,
            Reviews = b.Reviews
                .Select(r => new ReviewViewModel
                {
                    Id = r.Id,
                    UserName = r.User.UserName ?? "No name",
                    Rating = r.Rating,
                    Comment = r.Comment,
                    CreatedOn = r.CreatedOn
                })
                .ToList()
        })
        .FirstOrDefaultAsync();
        }

        public async Task<List<Book>> SearchBooksAsync(string query)
        {
            query = query.ToLower();

            return await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Genre)
                .Where(b =>
                    b.Title.ToLower().Contains(query) ||
                    b.Author.Name.ToLower().Contains(query) ||
                    b.Genre.Name.ToLower().Contains(query))
                .ToListAsync();
        }
    }
}
