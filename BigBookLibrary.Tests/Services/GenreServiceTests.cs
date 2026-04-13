using BigBookLibrary.Areas.Admin.ViewModels.Genres;
using BigBookLibrary.Data;
using BigBookLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace BigBookLibrary.Tests.Services
{
    public class GenreServiceTests
    {
        private ApplicationDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new ApplicationDbContext(options);
        }

        private GenreService GetService(ApplicationDbContext context)
        {
            return new GenreService(context);
        }

        // ---------------------------------------------------------
        // GetAllAsync
        // ---------------------------------------------------------
        [Fact]
        public async Task GetAllAsync_ReturnsOnlyNonDeletedGenres()
        {
            var context = GetDbContext();

            context.Genres.AddRange(
                new Genre { Id = 1, Name = "G1", IsDeleted = false },
                new Genre { Id = 2, Name = "G2", IsDeleted = true }
            );

            await context.SaveChangesAsync();

            var service = GetService(context);

            var result = await service.GetAllAsync();

            Assert.Single(result);
            Assert.Equal("G1", result.First().Name);
        }

        // ---------------------------------------------------------
        // GetByIdAsync
        // ---------------------------------------------------------
        [Fact]
        public async Task GetByIdAsync_ReturnsGenre_WhenExists()
        {
            var context = GetDbContext();

            context.Genres.Add(new Genre { Id = 1, Name = "Fantasy" });
            await context.SaveChangesAsync();

            var service = GetService(context);

            var result = await service.GetByIdAsync(1);

            Assert.NotNull(result);
            Assert.Equal("Fantasy", result.Name);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsNull_WhenDeleted()
        {
            var context = GetDbContext();

            context.Genres.Add(new Genre { Id = 1, Name = "Fantasy", IsDeleted = true });
            await context.SaveChangesAsync();

            var service = GetService(context);

            var result = await service.GetByIdAsync(1);

            Assert.Null(result);
        }

        // ---------------------------------------------------------
        // CreateAsync
        // ---------------------------------------------------------
        [Fact]
        public async Task CreateAsync_AddsGenreToDatabase()
        {
            var context = GetDbContext();
            var service = GetService(context);

            var model = new GenreFormModel { Name = "New Genre" };

            await service.CreateAsync(model);

            Assert.Equal(1, context.Genres.Count());
            Assert.Equal("New Genre", context.Genres.First().Name);
        }

        // ---------------------------------------------------------
        // EditAsync
        // ---------------------------------------------------------
        [Fact]
        public async Task EditAsync_UpdatesGenreName()
        {
            var context = GetDbContext();

            context.Genres.Add(new Genre { Id = 1, Name = "Old" });
            await context.SaveChangesAsync();

            var service = GetService(context);

            var model = new GenreFormModel { Name = "Updated" };

            await service.EditAsync(1, model);

            Assert.Equal("Updated", context.Genres.First().Name);
        }

        [Fact]
        public async Task EditAsync_DoesNothing_WhenGenreMissing()
        {
            var context = GetDbContext();
            var service = GetService(context);

            var model = new GenreFormModel { Name = "Updated" };

            await service.EditAsync(999, model);

            Assert.Empty(context.Genres);
        }

        // ---------------------------------------------------------
        // SoftDeleteAsync
        // ---------------------------------------------------------
        [Fact]
        public async Task SoftDeleteAsync_SetsIsDeletedTrue()
        {
            var context = GetDbContext();

            context.Genres.Add(new Genre { Id = 1, Name = "G1" });
            await context.SaveChangesAsync();

            var service = GetService(context);

            await service.SoftDeleteAsync(1);

            Assert.True(context.Genres.First().IsDeleted);
        }

        // ---------------------------------------------------------
        // GetBooksByGenreAsync
        // ---------------------------------------------------------
        [Fact]
        public async Task GetBooksByGenreAsync_ReturnsBooksOfGenre()
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
                    Genre = genre,
                    Author = author
                },
                new Book
                {
                    Id = 2,
                    Title = "Book 2",
                    ISBN = "222",
                    Year = 2001,
                    CopiesAvailable = 1,
                    AuthorId = 1,
                    GenreId = 2,
                    Genre = new Genre { Id = 2, Name = "Other" },
                    Author = author
                }
            );

            await context.SaveChangesAsync();

            var service = GetService(context);

            var result = await service.GetBooksByGenreAsync(1);

            Assert.Single(result);
            Assert.Equal("Book 1", result.First().Title);
        }

        [Fact]
        public async Task GetBooksByGenreAsync_ExcludesDeletedGenre()
        {
            var context = GetDbContext();

            var author = new Author { Id = 1, Name = "Author" };
            var genre = new Genre { Id = 1, Name = "Genre", IsDeleted = true };

            context.Authors.Add(author);
            context.Genres.Add(genre);

            context.Books.Add(new Book
            {
                Id = 1,
                Title = "Book 1",
                ISBN = "111",
                Year = 2000,
                CopiesAvailable = 1,
                AuthorId = 1,
                GenreId = 1,
                Genre = genre,
                Author = author
            });

            await context.SaveChangesAsync();

            var service = GetService(context);

            var result = await service.GetBooksByGenreAsync(1);

            Assert.Empty(result);
        }
    }
}

