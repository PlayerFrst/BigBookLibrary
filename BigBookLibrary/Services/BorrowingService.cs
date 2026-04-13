using BigBookLibrary.Data;
using BigBookLibrary.Models;
using BigBookLibrary.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BigBookLibrary.Services
{
    public class BorrowingService: IBorrowingService
    {
        private readonly ApplicationDbContext _context;

        public BorrowingService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> IsBookAvailable(int bookId)
        {
            var book = await _context.Books
                .FirstOrDefaultAsync(b => b.Id == bookId && !b.IsDeleted);

            if (book == null)
                return false;

            return book.CopiesAvailable > 0;
        }

        public async Task<bool> RentBookAsync(int bookId, string userId)
        {
            var book = await _context.Books
                .FirstOrDefaultAsync(b => b.Id == bookId && !b.IsDeleted);

            if (book == null)
                return false;

            if (book.CopiesAvailable <= 0)
                return false;

            var borrowing = new Borrowing
            {
                BookId = bookId,
                UserId = userId,
                BorrowedOn = DateTime.UtcNow,
                DueDate = DateTime.UtcNow.AddDays(14)
            };

            book.CopiesAvailable--;

            _context.Borrowings.Add(borrowing);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ReturnBookAsync(int borrowingId)
        {
            var borrowing = await _context.Borrowings
                .Include(b => b.Book)
                .FirstOrDefaultAsync(b => b.Id == borrowingId);

            if (borrowing == null || borrowing.ReturnedOn != null)
                return false;

            borrowing.ReturnedOn = DateTime.UtcNow;

            borrowing.Book.CopiesAvailable++;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Borrowing>> GetActiveBorrowingsForUser(string userId)
        {
            return await _context.Borrowings
                .Where(b => b.UserId == userId && b.ReturnedOn == null)
                .Include(b => b.Book)
                .ToListAsync();
        }

        public async Task<IEnumerable<Borrowing>> GetOverdueBorrowings()
        {
            var now = DateTime.UtcNow;

            return await _context.Borrowings
                .Where(b => b.ReturnedOn == null && b.DueDate < now)
                .Include(b => b.Book)
                .Include(b => b.User)
                .ToListAsync();
        }

        public async Task<bool> UserAlreadyBorrowedBook(string userId, int bookId)
        {
            return await _context.Borrowings
                .AnyAsync(b => b.UserId == userId
                            && b.BookId == bookId
                            && b.ReturnedOn == null);
        }

        public async Task<bool> UserHasOverdueBooks(string userId)
        {
            return await _context.Borrowings
                .AnyAsync(b => b.UserId == userId
                            && b.ReturnedOn == null
                            && b.DueDate < DateTime.UtcNow);
        }

    }
}
