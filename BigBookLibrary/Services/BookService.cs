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

        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            return await _context.Books
                .Where(b => !b.IsDeleted)
                .Include(b => b.Author)
                .Include(b => b.Genre)
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
            Description = b.Description,
            CoverImagePath = b.CoverImagePath,
            AuthorName = b.Author.Name,
            GenreName = b.Genre.Name,
            Reviews = b.Reviews
                .Select(r => new ReviewViewModel
                {
                    Id = r.Id,
                    UserName = r.User.UserName,
                    Rating = r.Rating,
                    Comment = r.Comment,
                    CreatedOn = r.CreatedOn
                })
                .ToList()
        })
        .FirstOrDefaultAsync();
        }
    }
}
