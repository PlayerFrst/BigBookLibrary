using BigBookLibrary.Data;
using BigBookLibrary.Models;
using BigBookLibrary.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BigBookLibrary.Tests.Services
{
    public class BorrowingServiceTests
    {
        private ApplicationDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new ApplicationDbContext(options);
        }

        private BorrowingService GetService(ApplicationDbContext context)
        {
            return new BorrowingService(context);
        }

        // ---------------------------------------------------------
        // IsBookAvailable
        // ---------------------------------------------------------
        [Fact]
        public async Task IsBookAvailable_ReturnsFalse_WhenBookMissing()
        {
            var context = GetDbContext();
            var service = GetService(context);

            var result = await service.IsBookAvailable(1);

            Assert.False(result);
        }

        [Fact]
        public async Task IsBookAvailable_ReturnsFalse_WhenDeleted()
        {
            var context = GetDbContext();

            context.Books.Add(new Book
            {
                Id = 1,
                Title = "Book",
                ISBN = "111",
                Year = 2000,
                CopiesAvailable = 1,
                IsDeleted = true
            });

            await context.SaveChangesAsync();

            var service = GetService(context);

            var result = await service.IsBookAvailable(1);

            Assert.False(result);
        }

        [Fact]
        public async Task IsBookAvailable_ReturnsTrue_WhenCopiesAvailable()
        {
            var context = GetDbContext();

            context.Books.Add(new Book
            {
                Id = 1,
                Title = "Book",
                ISBN = "111",
                Year = 2000,
                CopiesAvailable = 2
            });

            await context.SaveChangesAsync();

            var service = GetService(context);

            var result = await service.IsBookAvailable(1);

            Assert.True(result);
        }

        // ---------------------------------------------------------
        // RentBookAsync
        // ---------------------------------------------------------
        [Fact]
        public async Task RentBookAsync_ReturnsFalse_WhenBookMissing()
        {
            var context = GetDbContext();
            var service = GetService(context);

            var result = await service.RentBookAsync(1, "user1");

            Assert.False(result);
        }

        [Fact]
        public async Task RentBookAsync_ReturnsFalse_WhenNoCopies()
        {
            var context = GetDbContext();

            context.Books.Add(new Book
            {
                Id = 1,
                Title = "Book",
                ISBN = "111",
                Year = 2000,
                CopiesAvailable = 0
            });

            await context.SaveChangesAsync();

            var service = GetService(context);

            var result = await service.RentBookAsync(1, "user1");

            Assert.False(result);
        }

        [Fact]
        public async Task RentBookAsync_RentsBookAndDecreasesCopies()
        {
            var context = GetDbContext();

            context.Books.Add(new Book
            {
                Id = 1,
                Title = "Book",
                ISBN = "111",
                Year = 2000,
                CopiesAvailable = 3
            });

            await context.SaveChangesAsync();

            var service = GetService(context);

            var result = await service.RentBookAsync(1, "user1");

            Assert.True(result);
            Assert.Equal(2, context.Books.First().CopiesAvailable);
            Assert.Single(context.Borrowings);
        }

        // ---------------------------------------------------------
        // ReturnBookAsync
        // ---------------------------------------------------------
        [Fact]
        public async Task ReturnBookAsync_ReturnsFalse_WhenBorrowingMissing()
        {
            var context = GetDbContext();
            var service = GetService(context);

            var result = await service.ReturnBookAsync(1);

            Assert.False(result);
        }

        [Fact]
        public async Task ReturnBookAsync_ReturnsFalse_WhenAlreadyReturned()
        {
            var context = GetDbContext();

            var book = new Book
            {
                Id = 1,
                Title = "Book",
                ISBN = "111",
                Year = 2000,
                CopiesAvailable = 1
            };

            context.Books.Add(book);

            context.Borrowings.Add(new Borrowing
            {
                Id = 1,
                BookId = 1,
                Book = book,
                UserId = "user1",
                BorrowedOn = DateTime.UtcNow,
                ReturnedOn = DateTime.UtcNow
            });

            await context.SaveChangesAsync();

            var service = GetService(context);

            var result = await service.ReturnBookAsync(1);

            Assert.False(result);
        }

        [Fact]
        public async Task ReturnBookAsync_ReturnsBookAndIncreasesCopies()
        {
            var context = GetDbContext();

            var book = new Book
            {
                Id = 1,
                Title = "Book",
                ISBN = "111",
                Year = 2000,
                CopiesAvailable = 0
            };

            context.Books.Add(book);

            context.Borrowings.Add(new Borrowing
            {
                Id = 1,
                BookId = 1,
                Book = book,
                UserId = "user1",
                BorrowedOn = DateTime.UtcNow
            });

            await context.SaveChangesAsync();

            var service = GetService(context);

            var result = await service.ReturnBookAsync(1);

            Assert.True(result);
            Assert.Equal(1, context.Books.First().CopiesAvailable);
        }

        // ---------------------------------------------------------
        // GetActiveBorrowingsForUser
        // ---------------------------------------------------------
        [Fact]
        public async Task GetActiveBorrowingsForUser_ReturnsOnlyActive()
        {
            var context = GetDbContext();

            var book = new Book
            {
                Id = 1,
                Title = "Book",
                ISBN = "111",
                Year = 2000,
                CopiesAvailable = 1
            };

            context.Books.Add(book);

            context.Borrowings.AddRange(
                new Borrowing
                {
                    Id = 1,
                    BookId = 1,
                    Book = book,
                    UserId = "user1",
                    BorrowedOn = DateTime.UtcNow
                },
                new Borrowing
                {
                    Id = 2,
                    BookId = 1,
                    Book = book,
                    UserId = "user1",
                    BorrowedOn = DateTime.UtcNow,
                    ReturnedOn = DateTime.UtcNow
                }
            );

            await context.SaveChangesAsync();

            var service = GetService(context);

            var result = await service.GetActiveBorrowingsForUser("user1");

            Assert.Single(result);
            Assert.Equal(1, result.First().Id);
        }

        // ---------------------------------------------------------
        // GetOverdueBorrowings
        // ---------------------------------------------------------
        [Fact]
        public async Task GetOverdueBorrowings_ReturnsOnlyOverdue()
        {
            var context = GetDbContext();

            var user = new IdentityUser { Id = "u1", UserName = "Test" };
            var book = new Book
            {
                Id = 1,
                Title = "Book",
                ISBN = "111",
                Year = 2000,
                CopiesAvailable = 1
            };

            context.Users.Add(user);
            context.Books.Add(book);

            context.Borrowings.AddRange(
                new Borrowing
                {
                    Id = 1,
                    BookId = 1,
                    Book = book,
                    UserId = "u1",
                    User = user,
                    BorrowedOn = DateTime.UtcNow.AddDays(-20),
                    DueDate = DateTime.UtcNow.AddDays(-5)
                },
                new Borrowing
                {
                    Id = 2,
                    BookId = 1,
                    Book = book,
                    UserId = "u1",
                    User = user,
                    BorrowedOn = DateTime.UtcNow,
                    DueDate = DateTime.UtcNow.AddDays(5)
                }
            );

            await context.SaveChangesAsync();

            var service = GetService(context);

            var result = await service.GetOverdueBorrowings();

            Assert.Single(result);
            Assert.Equal(1, result.First().Id);
        }

        // ---------------------------------------------------------
        // UserAlreadyBorrowedBook
        // ---------------------------------------------------------
        [Fact]
        public async Task UserAlreadyBorrowedBook_ReturnsTrue_WhenActiveBorrowingExists()
        {
            var context = GetDbContext();

            context.Borrowings.Add(new Borrowing
            {
                Id = 1,
                BookId = 1,
                UserId = "user1",
                BorrowedOn = DateTime.UtcNow
            });

            await context.SaveChangesAsync();

            var service = GetService(context);

            var result = await service.UserAlreadyBorrowedBook("user1", 1);

            Assert.True(result);
        }

        [Fact]
        public async Task UserAlreadyBorrowedBook_ReturnsFalse_WhenReturned()
        {
            var context = GetDbContext();

            context.Borrowings.Add(new Borrowing
            {
                Id = 1,
                BookId = 1,
                UserId = "user1",
                BorrowedOn = DateTime.UtcNow,
                ReturnedOn = DateTime.UtcNow
            });

            await context.SaveChangesAsync();

            var service = GetService(context);

            var result = await service.UserAlreadyBorrowedBook("user1", 1);

            Assert.False(result);
        }

        // ---------------------------------------------------------
        // UserHasOverdueBooks
        // ---------------------------------------------------------
        [Fact]
        public async Task UserHasOverdueBooks_ReturnsTrue_WhenOverdueExists()
        {
            var context = GetDbContext();

            context.Borrowings.Add(new Borrowing
            {
                Id = 1,
                BookId = 1,
                UserId = "user1",
                BorrowedOn = DateTime.UtcNow.AddDays(-20),
                DueDate = DateTime.UtcNow.AddDays(-5)
            });

            await context.SaveChangesAsync();

            var service = GetService(context);

            var result = await service.UserHasOverdueBooks("user1");

            Assert.True(result);
        }

        [Fact]
        public async Task UserHasOverdueBooks_ReturnsFalse_WhenNoOverdue()
        {
            var context = GetDbContext();

            context.Borrowings.Add(new Borrowing
            {
                Id = 1,
                BookId = 1,
                UserId = "user1",
                BorrowedOn = DateTime.UtcNow,
                DueDate = DateTime.UtcNow.AddDays(5)
            });

            await context.SaveChangesAsync();

            var service = GetService(context);

            var result = await service.UserHasOverdueBooks("user1");

            Assert.False(result);
        }
    }
}
