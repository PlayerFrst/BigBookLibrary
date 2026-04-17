using BigBookLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BigBookLibrary.Data.Seeding.Configurations
{
    public class BorrowingConfiguration : IEntityTypeConfiguration<Borrowing>
    {
        public void Configure(EntityTypeBuilder<Borrowing> builder)
        {
            builder.HasData(
                // -------------------------
                // 1) ADMIN – OVERDUE BOOK
                // -------------------------
                new Borrowing
                {
                    Id = 1,
                    BorrowedOn = DateTime.UtcNow.AddDays(-20),
                    DueDate = DateTime.UtcNow.AddDays(-6),
                    ReturnedOn = null,
                    BookId = 2,
                    UserId = "2527ff55-6195-49b4-8f13-4f420775862d"
                },

                // -------------------------
                // 2) USER 1 – ACTIVE LOANS
                // -------------------------
                new Borrowing
                {
                    Id = 2,
                    BorrowedOn = DateTime.UtcNow.AddDays(-3),
                    DueDate = DateTime.UtcNow.AddDays(11),
                    ReturnedOn = null,
                    BookId = 3,
                    UserId = "22f27b10-8477-4db4-a0f1-024e9e7e369d"
                },
                new Borrowing
                {
                    Id = 3,
                    BorrowedOn = DateTime.UtcNow.AddDays(-5),
                    DueDate = DateTime.UtcNow.AddDays(9),
                    ReturnedOn = null,
                    BookId = 4,
                    UserId = "22f27b10-8477-4db4-a0f1-024e9e7e369d"
                },

                // -------------------------
                // 3) USER 1 – RETURNED LOANS
                // -------------------------
                new Borrowing
                {
                    Id = 4,
                    BorrowedOn = DateTime.UtcNow.AddDays(-12),
                    DueDate = DateTime.UtcNow.AddDays(2),
                    ReturnedOn = DateTime.UtcNow.AddDays(-2),
                    BookId = 5,
                    UserId = "22f27b10-8477-4db4-a0f1-024e9e7e369d"
                },
                new Borrowing
                {
                    Id = 5,
                    BorrowedOn = DateTime.UtcNow.AddDays(-15),
                    DueDate = DateTime.UtcNow.AddDays(-1),
                    ReturnedOn = DateTime.UtcNow.AddDays(-3),
                    BookId = 6,
                    UserId = "22f27b10-8477-4db4-a0f1-024e9e7e369d"
                },

                // -------------------------
                // 4) USER 2 – ACTIVE LOANS
                // -------------------------
                new Borrowing
                {
                    Id = 6,
                    BorrowedOn = DateTime.UtcNow.AddDays(-1),
                    DueDate = DateTime.UtcNow.AddDays(13),
                    ReturnedOn = null,
                    BookId = 7,
                    UserId = "e21751ac-1fa9-41f8-9026-acf50ca8d9fb"
                },
                new Borrowing
                {
                    Id = 7,
                    BorrowedOn = DateTime.UtcNow.AddDays(-4),
                    DueDate = DateTime.UtcNow.AddDays(10),
                    ReturnedOn = null,
                    BookId = 8,
                    UserId = "e21751ac-1fa9-41f8-9026-acf50ca8d9fb"
                },

                // -------------------------
                // 5) USER 2 – RETURNED LOANS
                // -------------------------
                new Borrowing
                {
                    Id = 8,
                    BorrowedOn = DateTime.UtcNow.AddDays(-18),
                    DueDate = DateTime.UtcNow.AddDays(-4),
                    ReturnedOn = DateTime.UtcNow.AddDays(-5),
                    BookId = 9,
                    UserId = "e21751ac-1fa9-41f8-9026-acf50ca8d9fb"
                },
                new Borrowing
                {
                    Id = 9,
                    BorrowedOn = DateTime.UtcNow.AddDays(-7),
                    DueDate = DateTime.UtcNow.AddDays(7),
                    ReturnedOn = DateTime.UtcNow.AddDays(-1),
                    BookId = 10,
                    UserId = "e21751ac-1fa9-41f8-9026-acf50ca8d9fb"
                }
            );
        }
    }
}
