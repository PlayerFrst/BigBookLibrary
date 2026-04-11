using BigBookLibrary.Models;

namespace BigBookLibrary.Services.Interfaces
{
    public interface IBorrowingService
    {
        Task<bool> IsBookAvailable(int bookId);
        Task<bool> RentBookAsync(int bookId, string userId);
        Task<bool> ReturnBookAsync(int borrowingId);
        Task<IEnumerable<Borrowing>> GetActiveBorrowingsForUser(string userId);
        Task<IEnumerable<Borrowing>> GetOverdueBorrowings();
    }
}
