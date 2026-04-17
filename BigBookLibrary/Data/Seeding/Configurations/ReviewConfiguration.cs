using BigBookLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BigBookLibrary.Data.Seeding.Configurations
{
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.HasData(
                // -------------------------
                // USER 1 REVIEWS
                // -------------------------
                new Review
                {
                    Id = 1,
                    Rating = 5,
                    Comment = "Amazing book! Highly recommended.",
                    CreatedOn = DateTime.UtcNow.AddDays(-10),
                    BookId = 2,
                    UserId = "22f27b10-8477-4db4-a0f1-024e9e7e369d"
                },
                new Review
                {
                    Id = 2,
                    Rating = 4,
                    Comment = "Very interesting and well written.",
                    CreatedOn = DateTime.UtcNow.AddDays(-7),
                    BookId = 3,
                    UserId = "22f27b10-8477-4db4-a0f1-024e9e7e369d"
                },
                new Review
                {
                    Id = 3,
                    Rating = 3,
                    Comment = "Good, but not great. Some parts were slow.",
                    CreatedOn = DateTime.UtcNow.AddDays(-3),
                    BookId = 4,
                    UserId = "22f27b10-8477-4db4-a0f1-024e9e7e369d"
                },

                // -------------------------
                // USER 2 REVIEWS
                // -------------------------
                new Review
                {
                    Id = 4,
                    Rating = 5,
                    Comment = "Loved every chapter!",
                    CreatedOn = DateTime.UtcNow.AddDays(-12),
                    BookId = 5,
                    UserId = "e21751ac-1fa9-41f8-9026-acf50ca8d9fb"
                },
                new Review
                {
                    Id = 5,
                    Rating = 2,
                    Comment = "Not my style, but others may enjoy it.",
                    CreatedOn = DateTime.UtcNow.AddDays(-6),
                    BookId = 6,
                    UserId = "e21751ac-1fa9-41f8-9026-acf50ca8d9fb"
                },
                new Review
                {
                    Id = 6,
                    Rating = 4,
                    Comment = "Solid read with great characters.",
                    CreatedOn = DateTime.UtcNow.AddDays(-2),
                    BookId = 7,
                    UserId = "e21751ac-1fa9-41f8-9026-acf50ca8d9fb"
                },

                // -------------------------
                // ADMIN REVIEWS
                // -------------------------
                new Review
                {
                    Id = 7,
                    Rating = 5,
                    Comment = "One of the best books in the library!",
                    CreatedOn = DateTime.UtcNow.AddDays(-15),
                    BookId = 8,
                    UserId = "2527ff55-6195-49b4-8f13-4f420775862d"
                },
                new Review
                {
                    Id = 8,
                    Rating = 4,
                    Comment = "Great pacing and strong plot.",
                    CreatedOn = DateTime.UtcNow.AddDays(-9),
                    BookId = 9,
                    UserId = "2527ff55-6195-49b4-8f13-4f420775862d"
                },
                new Review
                {
                    Id = 9,
                    Rating = 3,
                    Comment = "Decent book, but expected more.",
                    CreatedOn = DateTime.UtcNow.AddDays(-5),
                    BookId = 10,
                    UserId = "2527ff55-6195-49b4-8f13-4f420775862d"
                },
                new Review
                {
                    Id = 10,
                    Rating = 5,
                    Comment = "Fantastic! A must-read.",
                    CreatedOn = DateTime.UtcNow.AddDays(-1),
                    BookId = 11,
                    UserId = "2527ff55-6195-49b4-8f13-4f420775862d"
                }
            );
        }
    }
}
