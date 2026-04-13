using BigBookLibrary.Data;
using BigBookLibrary.Models;
using BigBookLibrary.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BigBookLibrary.Tests.Services
{
    public class BookServiceTests
    {
        private ApplicationDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new ApplicationDbContext(options);
        }

        private BookService GetService(ApplicationDbContext context)
        {
            return new BookService(context);
        }

        // ---------------------------------------------------------
        // GetAllBooksAsync
        // ---------------------------------------------------------
        [Fact]
        public async Task GetAllBooksAsync_ReturnsOnlyNonDeletedBooks()
        {
            var context = GetDbContext();

            var author = new Author { Id = 1, Name = "Author" };
            var genre = new Genre { Id = 1, Name = "Genre" };

            context.Authors.Add(author);
            context.Genres.Add(genre);

            context.Books.AddRange(
                new Book
                {
                    Id = 1,
                    Title = "Book 1",
                    ISBN = "111",
                    Year = 2000,
                    CopiesAvailable = 1,
                    AuthorId = 1,
                    GenreId = 1,
                    IsDeleted = false
                },
                new Book
                {
                    Id = 2,
                    Title = "Book 2",
                    ISBN = "222",
                    Year = 2001,
                    CopiesAvailable = 1,
                    AuthorId = 1,
                    GenreId = 1,
                    IsDeleted = true
                }
            );

            await context.SaveChangesAsync();

            var service = GetService(context);

            var result = await service.GetAllBooksAsync();

            Assert.Single(result);
            Assert.Equal("Book 1", result.First().Title);
        }

        // ---------------------------------------------------------
        // GetBookByIdAsync
        // ---------------------------------------------------------
        [Fact]
        public async Task GetBookByIdAsync_ReturnsBook_WhenExists()
        {
            var context = GetDbContext();

            var author = new Author { Id = 1, Name = "Author" };
            var genre = new Genre { Id = 1, Name = "Genre" };

            context.Authors.Add(author);
            context.Genres.Add(genre);

            var book = new Book
            {
                Id = 1,
                Title = "Book 1",
                ISBN = "111",
                Year = 2000,
                CopiesAvailable = 1,
                AuthorId = 1,
                GenreId = 1
            };

            context.Books.Add(book);
            await context.SaveChangesAsync();

            var service = GetService(context);

            var result = await service.GetBookByIdAsync(1);

            Assert.NotNull(result);
            Assert.Equal("Book 1", result.Title);
        }

        [Fact]
        public async Task GetBookByIdAsync_ReturnsNull_WhenDeleted()
        {
            var context = GetDbContext();

            var book = new Book
            {
                Id = 1,
                Title = "Book 1",
                ISBN = "111",
                Year = 2000,
                CopiesAvailable = 1,
                AuthorId = 1,
                GenreId = 1,
                IsDeleted = true
            };

            context.Books.Add(book);
            await context.SaveChangesAsync();

            var service = GetService(context);

            var result = await service.GetBookByIdAsync(1);

            Assert.Null(result);
        }

        // ---------------------------------------------------------
        // CreateBookAsync
        // ---------------------------------------------------------
        [Fact]
        public async Task CreateBookAsync_AddsBookToDatabase()
        {
            var context = GetDbContext();
            var service = GetService(context);

            var book = new Book
            {
                Id = 1,
                Title = "New Book",
                ISBN = "111",
                Year = 2020,
                CopiesAvailable = 5,
                AuthorId = 1,
                GenreId = 1
            };

            await service.CreateBookAsync(book);

            Assert.Equal(1, context.Books.Count());
        }

        // ---------------------------------------------------------
        // UpdateBookAsync
        // ---------------------------------------------------------
        [Fact]
        public async Task UpdateBookAsync_UpdatesBook()
        {
            var context = GetDbContext();
            var service = GetService(context);

            var book = new Book
            {
                Id = 1,
                Title = "Old Title",
                ISBN = "111",
                Year = 2020,
                CopiesAvailable = 5,
                AuthorId = 1,
                GenreId = 1
            };

            context.Books.Add(book);
            await context.SaveChangesAsync();

            book.Title = "Updated Title";

            await service.UpdateBookAsync(book);

            Assert.Equal("Updated Title", context.Books.First().Title);
        }

        // ---------------------------------------------------------
        // DeleteBookAsync
        // ---------------------------------------------------------
        [Fact]
        public async Task DeleteBookAsync_SetsIsDeletedTrue()
        {
            var context = GetDbContext();
            var service = GetService(context);

            var book = new Book
            {
                Id = 1,
                Title = "Book",
                ISBN = "111",
                Year = 2020,
                CopiesAvailable = 5,
                AuthorId = 1,
                GenreId = 1
            };

            context.Books.Add(book);
            await context.SaveChangesAsync();

            await service.DeleteBookAsync(book);

            Assert.True(context.Books.First().IsDeleted);
        }

        // ---------------------------------------------------------
        // GetBookDetailsAsync
        // ---------------------------------------------------------
        [Fact]
        public async Task GetBookDetailsAsync_ReturnsDetailsWithReviews()
        {
            var context = GetDbContext();

            var author = new Author { Id = 1, Name = "Author" };
            var genre = new Genre { Id = 1, Name = "Genre" };
            var user = new IdentityUser { Id = "u1", UserName = "TestUser" };


            context.Authors.Add(author);
            context.Genres.Add(genre);
            context.Users.Add(user);

            var book = new Book
            {
                Id = 1,
                Title = "Book",
                ISBN = "111",
                Year = 2020,
                CopiesAvailable = 5,
                AuthorId = 1,
                GenreId = 1,
                Reviews = new List<Review>
                {
                    new Review
                    {
                        Id = 1,
                        Rating = 5,
                        Comment = "Great!",
                        CreatedOn = DateTime.UtcNow,
                        UserId = "u1",
                        User = user
                    }
                }
            };

            context.Books.Add(book);
            await context.SaveChangesAsync();

            var service = GetService(context);

            var result = await service.GetBookDetailsAsync(1);

            Assert.NotNull(result);
            Assert.Single(result.Reviews);
            Assert.Equal("TestUser", result.Reviews.First().UserName);
        }

        // ---------------------------------------------------------
        // SearchBooksAsync
        // ---------------------------------------------------------
        [Fact]
        public async Task SearchBooksAsync_FindsBooksByTitleAuthorOrGenre()
        {
            var context = GetDbContext();

            var author = new Author { Id = 1, Name = "AuthorName" };
            var genre = new Genre { Id = 1, Name = "GenreName" };

            context.Authors.Add(author);
            context.Genres.Add(genre);

            context.Books.Add(new Book
            {
                Id = 1,
                Title = "Special Book",
                ISBN = "111",
                Year = 2020,
                CopiesAvailable = 5,
                AuthorId = 1,
                GenreId = 1
            });

            await context.SaveChangesAsync();

            var service = GetService(context);

            var result = await service.SearchBooksAsync("special");

            Assert.Single(result);
        }
    }
}
