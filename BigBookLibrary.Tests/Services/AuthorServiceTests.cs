using BigBookLibrary.Areas.Admin.ViewModels.Authors;
using BigBookLibrary.Data;
using BigBookLibrary.Models;
using BigBookLibrary.Services;
using Microsoft.EntityFrameworkCore;

namespace BigBookLibrary.Tests.Services
{
    public class AuthorServiceTests
    {
        private ApplicationDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()) 
                .Options;

            return new ApplicationDbContext(options);
        }

        private AuthorService GetService(ApplicationDbContext context)
        {
            return new AuthorService(context);
        }

        // ---------------------------------------------------------
        // GetAllAsync
        // ---------------------------------------------------------
        [Fact]
        public async Task GetAllAsync_ReturnsOnlyNotDeletedAuthors()
        {
            var context = GetDbContext();

            context.Authors.AddRange(
                new Author { Id = 1, Name = "Author 1", IsDeleted = false },
                new Author { Id = 2, Name = "Author 2", IsDeleted = true }
            );

            await context.SaveChangesAsync();

            var service = GetService(context);

            var result = await service.GetAllAsync();

            Assert.Single(result);
            Assert.Equal("Author 1", result.First().Name);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsCorrectBooksCount()
        {
            var context = GetDbContext();

            var author = new Author
            {
                Id = 1,
                Name = "Author",
                Books = new List<Book>
        {
            new Book
            {
                Id = 1,
                Title = "Book 1",
                ISBN = "1234567890",
                Year = 2000,
                CopiesAvailable = 5,
                AuthorId = 1,
                GenreId = 1,
                Genre = new Genre { Id = 1, Name = "G1" },
                Author = new Author { Id = 1, Name = "A1" },
                IsDeleted = false
            },
            new Book
            {
                Id = 2,
                Title = "Book 2",
                ISBN = "12341237890",
                Year = 2001,
                CopiesAvailable = 3,
                AuthorId = 1,
                GenreId = 3,
                Genre = new Genre { Id = 3, Name = "G3" },
                Author = new Author { Id = 1, Name = "A1" },
                IsDeleted = true  
            }
        }
            };

            context.Authors.Add(author);
            await context.SaveChangesAsync();

            var service = GetService(context);

            var result = await service.GetAllAsync();
            var vm = result.First();

            Assert.Equal(1, vm.BooksCount);
        }


        // ---------------------------------------------------------
        // GetByIdAsync
        // ---------------------------------------------------------
        [Fact]
        public async Task GetByIdAsync_ReturnsNull_WhenAuthorNotFound()
        {
            var context = GetDbContext();
            var service = GetService(context);

            var result = await service.GetByIdAsync(999);

            Assert.Null(result);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsCorrectModel()
        {
            var context = GetDbContext();

            context.Authors.Add(new Author
            {
                Id = 1,
                Name = "Test Author",
                Biography = "Bio"
            });

            await context.SaveChangesAsync();

            var service = GetService(context);

            var result = await service.GetByIdAsync(1);

            Assert.NotNull(result);
            Assert.Equal("Test Author", result!.Name);
            Assert.Equal("Bio", result.Biography);
        }

        // ---------------------------------------------------------
        // CreateAsync
        // ---------------------------------------------------------
        [Fact]
        public async Task CreateAsync_AddsAuthorToDatabase()
        {
            var context = GetDbContext();
            var service = GetService(context);

            var model = new AuthorFormModel
            {
                Name = "New Author",
                Biography = "Bio"
            };

            await service.CreateAsync(model);

            Assert.Equal(1, context.Authors.Count());
            Assert.Equal("New Author", context.Authors.First().Name);
        }

        // ---------------------------------------------------------
        // EditAsync
        // ---------------------------------------------------------
        [Fact]
        public async Task EditAsync_UpdatesExistingAuthor()
        {
            var context = GetDbContext();

            context.Authors.Add(new Author
            {
                Id = 1,
                Name = "Old Name",
                Biography = "Old Bio"
            });

            await context.SaveChangesAsync();

            var service = GetService(context);

            var model = new AuthorFormModel
            {
                Name = "New Name",
                Biography = "New Bio"
            };

            await service.EditAsync(1, model);

            var updated = context.Authors.First();

            Assert.Equal("New Name", updated.Name);
            Assert.Equal("New Bio", updated.Biography);
        }

        [Fact]
        public async Task EditAsync_DoesNothing_WhenAuthorNotFound()
        {
            var context = GetDbContext();
            var service = GetService(context);

            var model = new AuthorFormModel
            {
                Name = "Name",
                Biography = "Bio"
            };

            await service.EditAsync(999, model);

            Assert.Empty(context.Authors); 
        }

        // ---------------------------------------------------------
        // SoftDeleteAsync
        // ---------------------------------------------------------
        [Fact]
        public async Task SoftDeleteAsync_SetsIsDeletedToTrue()
        {
            var context = GetDbContext();

            context.Authors.Add(new Author { Id = 1, Name = "Author" });
            await context.SaveChangesAsync();

            var service = GetService(context);

            await service.SoftDeleteAsync(1);

            Assert.True(context.Authors.First().IsDeleted);
        }

        [Fact]
        public async Task SoftDeleteAsync_DoesNothing_WhenAuthorNotFound()
        {
            var context = GetDbContext();
            var service = GetService(context);

            await service.SoftDeleteAsync(999);

            Assert.Empty(context.Authors);
        }

        // ---------------------------------------------------------
        // GetBooksByAuthorAsync
        // ---------------------------------------------------------
        [Fact]
        public async Task GetBooksByAuthorAsync_ReturnsOnlyBooksOfAuthor()
        {
            var context = GetDbContext();

            
            var author = new Author { Id = 1, Name = "A1" };
            var genre1 = new Genre { Id = 1, Name = "G1" };
            var genre2 = new Genre { Id = 2, Name = "G2" };

            context.Authors.Add(author);
            context.Genres.AddRange(genre1, genre2);

           
            context.Books.AddRange(
                new Book
                {
                    Id = 1,
                    Title = "Book 1",
                    ISBN = "123",
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
                    ISBN = "1223",
                    Year = 2004,
                    CopiesAvailable = 3,
                    AuthorId = 2, 
                    GenreId = 2,
                    IsDeleted = false
                }
            );

            await context.SaveChangesAsync();

            var service = GetService(context);

            var result = await service.GetBooksByAuthorAsync(1);

            Assert.Single(result);
            Assert.Equal("Book 1", result.First().Title);
        }


        [Fact]
        public async Task GetBooksByAuthorAsync_ExcludesDeletedBooks()
        {
            var context = GetDbContext();

           
            var author = new Author { Id = 1, Name = "A1" };
            var genre = new Genre { Id = 1, Name = "G1" };

            context.Authors.Add(author);
            context.Genres.Add(genre);

            
            context.Books.AddRange(
                new Book
                {
                    Id = 1,
                    Title = "Book 1",
                    ISBN = "123",
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
                    ISBN = "1233",
                    Year = 2000,
                    CopiesAvailable = 2,
                    AuthorId = 1,   
                    GenreId = 1, 
                    IsDeleted = true 
                }
            );

            await context.SaveChangesAsync();

            var service = GetService(context);

            var result = await service.GetBooksByAuthorAsync(1);

            Assert.Single(result);
            Assert.Equal("Book 1", result.First().Title);
        }


        // ---------------------------------------------------------
        // GetAuthorEntityByIdAsync
        // ---------------------------------------------------------
        [Fact]
        public async Task GetAuthorEntityByIdAsync_ReturnsAuthorWithBooks()
        {
            var context = GetDbContext();

            context.Authors.Add(new Author
            {
                Id = 1,
                Name = "Author",
                Books = new List<Book>
                {
                     new Book
                     {
                         Id = 1,
                         Title = "Book 1",
                         ISBN = "123",
                         Year = 2000,
                         CopiesAvailable = 1,
                         AuthorId = 1,
                         GenreId = 1,
                         Genre = new Genre { Id = 1, Name = "G1" }
                     }
                }
            });

            await context.SaveChangesAsync();

            var service = GetService(context);

            var result = await service.GetAuthorEntityByIdAsync(1);

            Assert.NotNull(result);
            Assert.Single(result!.Books);
        }

        [Fact]
        public async Task GetAuthorEntityByIdAsync_ReturnsNull_WhenDeleted()
        {
            var context = GetDbContext();

            context.Authors.Add(new Author
            {
                Id = 1,
                Name = "Author",
                IsDeleted = true
            });

            await context.SaveChangesAsync();

            var service = GetService(context);

            var result = await service.GetAuthorEntityByIdAsync(1);

            Assert.Null(result);
        }
    }
}
