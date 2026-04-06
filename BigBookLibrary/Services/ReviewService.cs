using BigBookLibrary.Data;
using BigBookLibrary.Models;
using BigBookLibrary.Services.Interfaces;
using BigBookLibrary.ViewModels.Reviews;
using Microsoft.EntityFrameworkCore;

namespace BigBookLibrary.Services
{
    public class ReviewService: IReviewService
    {
        private readonly ApplicationDbContext _context;

        public ReviewService(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task AddReviewAsync(int bookId, string userId, ReviewFormModel model)
        {
            var review = new Review
            {
                BookId = bookId,
                UserId = userId,
                Rating = model.Rating,
                Comment = model.Comment,
                CreatedOn = DateTime.UtcNow
            };

            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> CanUserDeleteReviewAsync(int reviewId, string userId)
        {
            var review = await _context.Reviews.FindAsync(reviewId);

            if (review == null)
                return false;

            return review.UserId == userId;
        }

        public async Task DeleteReviewAsync(int reviewId)
        {
            var review = await _context.Reviews.FindAsync(reviewId);

            if (review != null)
            {
                _context.Reviews.Remove(review);
                await _context.SaveChangesAsync();
            }
        }

        public  async Task<List<ReviewViewModel>> GetReviewsForBookAsync(int bookId)
        {
            return await _context.Reviews
       .Where(r => r.BookId == bookId)
       .OrderByDescending(r => r.CreatedOn)
       .Select(r => new ReviewViewModel
       {
           Id = r.Id,
           UserId = r.UserId,
           UserName = r.User.UserName,
           Rating = r.Rating,
           Comment = r.Comment,
           CreatedOn = r.CreatedOn
       })
       .ToListAsync();
        }
    }
}
