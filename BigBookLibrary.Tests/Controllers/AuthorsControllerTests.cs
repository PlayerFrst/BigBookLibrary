using BigBookLibrary.Areas.Admin.Controllers;
using BigBookLibrary.Areas.Admin.ViewModels.Authors;
using BigBookLibrary.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace BigBookLibrary.Tests.Controllers
{
    public class AuthorsControllerTests
    {
        private readonly Mock<IAuthorService> _authorServiceMock;
        private readonly AuthorsController _controller;

        public AuthorsControllerTests()
        {
            _authorServiceMock = new Mock<IAuthorService>();
            _controller = new AuthorsController(_authorServiceMock.Object);
        }

        // ---------------------------------------------------------
        // Index()
        // ---------------------------------------------------------
        [Fact]
        public async Task Index_ReturnsViewWithAuthors()
        {
            // Arrange
            var authors = new List<AuthorViewModel>
            {
                new AuthorViewModel { Id = 1, Name = "Author 1" }
            };

            _authorServiceMock
                .Setup(s => s.GetAllAsync())
                .ReturnsAsync(authors);

            // Act
            var result = await _controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(authors, viewResult.Model);
        }

        // ---------------------------------------------------------
        // Create() GET
        // ---------------------------------------------------------
        [Fact]
        public void Create_Get_ReturnsViewWithEmptyModel()
        {
            // Act
            var result = _controller.Create();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsType<AuthorFormModel>(viewResult.Model);
        }

        // ---------------------------------------------------------
        // Create() POST
        // ---------------------------------------------------------
        [Fact]
        public async Task Create_Post_InvalidModel_ReturnsView()
        {
            // Arrange
            _controller.ModelState.AddModelError("Name", "Required");

            var model = new AuthorFormModel();

            // Act
            var result = await _controller.Create(model);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(model, viewResult.Model);
        }

        [Fact]
        public async Task Create_Post_ValidModel_RedirectsToIndex()
        {
            // Arrange
            var model = new AuthorFormModel
            {
                Name = "Test",
                Biography = "Bio"
            };

            // Act
            var result = await _controller.Create(model);

            // Assert
            _authorServiceMock.Verify(s => s.CreateAsync(model), Times.Once);

            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirect.ActionName);
        }

        [Fact]
        public async Task Create_Post_WithReturnUrl_RedirectsToReturnUrl()
        {
            // Arrange
            var model = new AuthorFormModel
            {
                Name = "Test",
                Biography = "Bio"
            };

            string returnUrl = "/Admin/Authors/Index";

            // Act
            var result = await _controller.Create(model, returnUrl);

            // Assert
            var redirect = Assert.IsType<RedirectResult>(result);
            Assert.Equal(returnUrl, redirect.Url);
        }

        // ---------------------------------------------------------
        // Edit() GET
        // ---------------------------------------------------------
        [Fact]
        public async Task Edit_Get_ReturnsNotFound_WhenAuthorMissing()
        {
            // Arrange
            _authorServiceMock
                .Setup(s => s.GetByIdAsync(1))
                .ReturnsAsync((AuthorFormModel?)null);

            // Act
            var result = await _controller.Edit(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Edit_Get_ReturnsViewWithModel()
        {
            // Arrange
            var model = new AuthorFormModel { Name = "Test" };

            _authorServiceMock
                .Setup(s => s.GetByIdAsync(1))
                .ReturnsAsync(model);

            // Act
            var result = await _controller.Edit(1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(model, viewResult.Model);
        }

        // ---------------------------------------------------------
        // Edit() POST
        // ---------------------------------------------------------
        [Fact]
        public async Task Edit_Post_InvalidModel_ReturnsView()
        {
            // Arrange
            _controller.ModelState.AddModelError("Name", "Required");

            var model = new AuthorFormModel();

            // Act
            var result = await _controller.Edit(1, model);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(model, viewResult.Model);
        }

        [Fact]
        public async Task Edit_Post_ValidModel_RedirectsToIndex()
        {
            // Arrange
            var model = new AuthorFormModel
            {
                Name = "Updated",
                Biography = "Bio"
            };

            // Act
            var result = await _controller.Edit(1, model);

            // Assert
            _authorServiceMock.Verify(s => s.EditAsync(1, model), Times.Once);

            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirect.ActionName);
        }

        // ---------------------------------------------------------
        // Delete() POST
        // ---------------------------------------------------------
        [Fact]
        public async Task Delete_Post_CallsServiceAndRedirects()
        {
            // Act
            var result = await _controller.Delete(1);

            // Assert
            _authorServiceMock.Verify(s => s.SoftDeleteAsync(1), Times.Once);

            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirect.ActionName);
        }
    }
}
