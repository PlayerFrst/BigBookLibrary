using BigBookLibrary.Data;
using BigBookLibrary.Models;
using BigBookLibrary.Services;
using BigBookLibrary.ViewModels.Reviews;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BigBookLibrary.Tests.Services
{
    public class ReviewServiceTests
    {
        private ApplicationDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new ApplicationDbContext(options);
        }

        private ReviewService GetService(ApplicationDbContext context)
        {
            return new ReviewService(context);
        }

        // ---------------------------------------------------------
        // AddReviewAsync
        // ---------------------------------------------------------
        [Fact]
        public async Task AddReviewAsync_AddsReviewToDatabase()
        {
            var context = GetDbContext();
            var service = GetService(context);

            var model = new ReviewFormModel
            {
                Rating = 5,
                Comment = "Great book!"
            };

            await service.AddReviewAsync(1, "user1", model);

            Assert.Equal(1, context.Reviews.Count());
            var review = context.Reviews.First();

            Assert.Equal(1, review.BookId);
            Assert.Equal("user1", review.UserId);
            Assert.Equal(5, review.Rating);
            Assert.Equal("Great book!", review.Comment);
        }

        // ---------------------------------------------------------
        // CanUserDeleteReviewAsync
        // ---------------------------------------------------------
        [Fact]
        public async Task CanUserDeleteReviewAsync_ReturnsTrue_WhenUserOwnsReview()
        {
            var context = GetDbContext();

            context.Reviews.Add(new Review
            {
                Id = 1,
                BookId = 1,
                UserId = "user1",
                Rating = 4,
                Comment = "Nice"
            });

            await context.SaveChangesAsync();

            var service = GetService(context);

            var result = await service.CanUserDeleteReviewAsync(1, "user1");

            Assert.True(result);
        }

        [Fact]
        public async Task CanUserDeleteReviewAsync_ReturnsFalse_WhenUserDoesNotOwnReview()
        {
            var context = GetDbContext();

            context.Reviews.Add(new Review
            {
                Id = 1,
                BookId = 1,
                UserId = "user1",
                Rating = 4,
                Comment = "test"
            });

            await context.SaveChangesAsync();

            var service = GetService(context);

            var result = await service.CanUserDeleteReviewAsync(1, "otherUser");

            Assert.False(result);
        }


        [Fact]
        public async Task CanUserDeleteReviewAsync_ReturnsFalse_WhenReviewDoesNotExist()
        {
            var context = GetDbContext();
            var service = GetService(context);

            var result = await service.CanUserDeleteReviewAsync(999, "user1");

            Assert.False(result);
        }

        // ---------------------------------------------------------
        // DeleteReviewAsync
        // ---------------------------------------------------------
        [Fact]
        public async Task DeleteReviewAsync_RemovesReview()
        {
            var context = GetDbContext();

            context.Reviews.Add(new Review
            {
                Id = 1,
                BookId = 1,
                UserId = "user1",
                Rating = 4,
                Comment = "test"
            });

            await context.SaveChangesAsync();

            var service = GetService(context);

            await service.DeleteReviewAsync(1);

            Assert.Empty(context.Reviews);
        }


        [Fact]
        public async Task DeleteReviewAsync_DoesNothing_WhenReviewMissing()
        {
            var context = GetDbContext();
            var service = GetService(context);

            await service.DeleteReviewAsync(999);

            Assert.Empty(context.Reviews);
        }

        // ---------------------------------------------------------
        // GetReviewsForBookAsync
        // ---------------------------------------------------------
        [Fact]
        public async Task GetReviewsForBookAsync_ReturnsReviewsOrderedByDate()
        {
            var context = GetDbContext();

            var user = new IdentityUser { Id = "u1", UserName = "TestUser" };
            context.Users.Add(user);

            context.Reviews.AddRange(
                new Review
                {
                    Id = 1,
                    BookId = 1,
                    UserId = "u1",
                    User = user,
                    Rating = 5,
                    Comment = "Newer",
                    CreatedOn = DateTime.UtcNow
                },
                new Review
                {
                    Id = 2,
                    BookId = 1,
                    UserId = "u1",
                    User = user,
                    Rating = 4,
                    Comment = "Older",
                    CreatedOn = DateTime.UtcNow.AddHours(-1)
                }
            );

            await context.SaveChangesAsync();

            var service = GetService(context);

            var result = await service.GetReviewsForBookAsync(1);

            Assert.Equal(2, result.Count);
            Assert.Equal("Newer", result[0].Comment);
            Assert.Equal("Older", result[1].Comment);
        }

        [Fact]
        public async Task GetReviewsForBookAsync_ReturnsEmptyList_WhenNoReviews()
        {
            var context = GetDbContext();
            var service = GetService(context);

            var result = await service.GetReviewsForBookAsync(1);

            Assert.Empty(result);
        }
    }
}
