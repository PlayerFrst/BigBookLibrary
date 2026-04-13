using BigBookLibrary.Areas.Admin.Controllers;
using BigBookLibrary.Areas.Admin.ViewModels.Authors;
using BigBookLibrary.Areas.Admin.ViewModels.Books;
using BigBookLibrary.Areas.Admin.ViewModels.Genres;
using BigBookLibrary.Models;
using BigBookLibrary.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using BookDetailsViewModel = BigBookLibrary.Areas.Admin.ViewModels.Books.BookDetailsViewModel;

namespace BigBookLibrary.Tests.Controllers
{
    public class BooksControllerTests
    {
        private Mock<IBookService> _bookService;
        private Mock<IAuthorService> _authorService;
        private Mock<IGenreService> _genreService;

        private BooksController GetController()
        {
            _bookService = new Mock<IBookService>();
            _authorService = new Mock<IAuthorService>();
            _genreService = new Mock<IGenreService>();

            return new BooksController(_bookService.Object, _authorService.Object, _genreService.Object);
        }

        // ---------------------------------------------------------
        // DETAILS
        // ---------------------------------------------------------
        [Fact]
        public async Task Details_ReturnsNotFound_WhenBookMissing()
        {
            var controller = GetController();

            _bookService.Setup(s => s.GetBookByIdAsync(1))
                .ReturnsAsync((Book?)null);

            var result = await controller.Details(1);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Details_ReturnsViewWithModel()
        {
            var controller = GetController();

            _bookService.Setup(s => s.GetBookByIdAsync(1))
                .ReturnsAsync(new Book
                {
                    Id = 1,
                    Title = "Book",
                    ISBN = "123",
                    Year = 2000,
                    CopiesAvailable = 1,
                    Author = new Author { Name = "Author" },
                    Genre = new Genre { Name = "Genre" }
                });

            var result = await controller.Details(1);

            var view = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<BookDetailsViewModel>(view.Model);

            Assert.Equal("Book", model.Title);
        }

        // ---------------------------------------------------------
        // CREATE GET
        // ---------------------------------------------------------
        [Fact]
        public async Task Create_Get_ReturnsViewWithDropdowns()
        {
            var controller = GetController();

            _authorService.Setup(s => s.GetAllAsync())
                .ReturnsAsync(new List<AuthorViewModel> { new AuthorViewModel { Id = 1, Name = "A1" } });

            _genreService.Setup(s => s.GetAllAsync())
                .ReturnsAsync(new List<GenreViewModel> { new GenreViewModel { Id = 1, Name = "G1" } });

            var result = await controller.Create();

            var view = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<BookFormViewModel>(view.Model);

            Assert.Single(model.Authors);
            Assert.Single(model.Genres);
        }

        // ---------------------------------------------------------
        // CREATE POST
        // ---------------------------------------------------------
        [Fact]
        public async Task Create_Post_InvalidModel_ReturnsView()
        {
            var controller = GetController();
            controller.ModelState.AddModelError("Title", "Required");

            var model = new BookFormViewModel();

            var result = await controller.Create(model);

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task Create_Post_Valid_Redirects()
        {
            var controller = GetController();

            var model = new BookFormViewModel
            {
                Title = "Book",
                ISBN = "123",
                Year = 2000,
                CopiesAvailable = 1,
                AuthorId = 1,
                GenreId = 1
            };

            var result = await controller.Create(model);

            Assert.IsType<RedirectToActionResult>(result);
        }

        // ---------------------------------------------------------
        // EDIT GET
        // ---------------------------------------------------------
        [Fact]
        public async Task Edit_Get_ReturnsNotFound_WhenMissing()
        {
            var controller = GetController();

            _bookService.Setup(s => s.GetBookByIdAsync(1))
                .ReturnsAsync((Book?)null);

            var result = await controller.Edit(1);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Edit_Get_ReturnsViewWithModel()
        {
            var controller = GetController();

            _bookService.Setup(s => s.GetBookByIdAsync(1))
                .ReturnsAsync(new Book
                {
                    Id = 1,
                    Title = "Book",
                    ISBN = "123",
                    Year = 2000,
                    CopiesAvailable = 1,
                    AuthorId = 1,
                    GenreId = 1,
                    Author = new Author { Name = "A1" },
                    Genre = new Genre { Name = "G1" }
                });

            _authorService.Setup(s => s.GetAllAsync())
                .ReturnsAsync(new List<AuthorViewModel> { new AuthorViewModel { Id = 1, Name = "A1" } });

            _genreService.Setup(s => s.GetAllAsync())
                .ReturnsAsync(new List<GenreViewModel> { new GenreViewModel { Id = 1, Name = "G1" } });

            var result = await controller.Edit(1);

            Assert.IsType<ViewResult>(result);
        }

        // ---------------------------------------------------------
        // EDIT POST
        // ---------------------------------------------------------
        [Fact]
        public async Task Edit_Post_NotFound_WhenMissing()
        {
            var controller = GetController();

            _bookService.Setup(s => s.GetBookByIdAsync(1))
                .ReturnsAsync((Book?)null);

            var model = new BookFormViewModel();

            var result = await controller.Edit(1, model);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Edit_Post_Valid_Redirects()
        {
            var controller = GetController();

            _bookService.Setup(s => s.GetBookByIdAsync(1))
                .ReturnsAsync(new Book { Id = 1 });

            var model = new BookFormViewModel
            {
                Title = "Updated",
                ISBN = "123",
                Year = 2000,
                CopiesAvailable = 1,
                AuthorId = 1,
                GenreId = 1
            };

            var result = await controller.Edit(1, model);

            Assert.IsType<RedirectToActionResult>(result);
        }

        // ---------------------------------------------------------
        // DELETE GET
        // ---------------------------------------------------------
        [Fact]
        public async Task Delete_Get_ReturnsNotFound_WhenMissing()
        {
            var controller = GetController();

            _bookService.Setup(s => s.GetBookByIdAsync(1))
                .ReturnsAsync((Book?)null);

            var result = await controller.Delete(1);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Delete_Get_ReturnsView()
        {
            var controller = GetController();

            _bookService.Setup(s => s.GetBookByIdAsync(1))
                .ReturnsAsync(new Book
                {
                    Id = 1,
                    Title = "Book",
                    Year = 2000,
                    Author = new Author { Name = "A1" }
                });

            var result = await controller.Delete(1);

            Assert.IsType<ViewResult>(result);
        }

        // ---------------------------------------------------------
        // DELETE POST
        // ---------------------------------------------------------
        [Fact]
        public async Task Delete_Post_Redirects()
        {
            var controller = GetController();

            _bookService.Setup(s => s.GetBookByIdAsync(1))
                .ReturnsAsync(new Book { Id = 1 });

            var model = new BookDeleteViewModel { Id = 1 };

            var result = await controller.Delete(model);

            Assert.IsType<RedirectToActionResult>(result);
        }
    }
}
