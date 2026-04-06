using BigBookLibrary.ViewModels.Reviews;

namespace BigBookLibrary.Services.Interfaces
{
    public interface IReviewService
    {
        Task AddReviewAsync(int bookId, string userId, ReviewFormModel model);
        Task<bool> CanUserDeleteReviewAsync(int reviewId, string userId);
        Task DeleteReviewAsync(int reviewId);
        Task<List<ReviewViewModel>> GetReviewsForBookAsync(int bookId);
    }
}
