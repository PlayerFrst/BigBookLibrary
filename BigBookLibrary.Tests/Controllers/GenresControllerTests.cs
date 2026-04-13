using BigBookLibrary.Areas.Admin.Controllers;
using BigBookLibrary.Areas.Admin.ViewModels.Genres;
using BigBookLibrary.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace BigBookLibrary.Tests.Controllers
{
    public class GenresControllerTests
    {
        private Mock<IGenreService> _genreService;

        private GenresController GetController()
        {
            _genreService = new Mock<IGenreService>();
            return new GenresController(_genreService.Object);
        }

        // ---------------------------------------------------------
        // INDEX
        // ---------------------------------------------------------
        [Fact]
        public async Task Index_ReturnsViewWithGenres()
        {
            var controller = GetController();

            _genreService.Setup(s => s.GetAllAsync())
                .ReturnsAsync(new List<GenreViewModel>
                {
                    new GenreViewModel { Id = 1, Name = "Fantasy" }
                });

            var result = await controller.Index();

            var view = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<GenreViewModel>>(view.Model);

            Assert.Single(model);
        }

        // ---------------------------------------------------------
        // CREATE GET
        // ---------------------------------------------------------
        [Fact]
        public void Create_Get_ReturnsView()
        {
            var controller = GetController();

            var result = controller.Create();

            var view = Assert.IsType<ViewResult>(result);
            Assert.IsType<GenreFormModel>(view.Model);
        }

        // ---------------------------------------------------------
        // CREATE POST
        // ---------------------------------------------------------
        [Fact]
        public async Task Create_Post_InvalidModel_ReturnsView()
        {
            var controller = GetController();
            controller.ModelState.AddModelError("Name", "Required");

            var model = new GenreFormModel();

            var result = await controller.Create(model);

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task Create_Post_Valid_RedirectsToIndex()
        {
            var controller = GetController();

            var model = new GenreFormModel { Name = "New Genre" };

            var result = await controller.Create(model);

            Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", ((RedirectToActionResult)result).ActionName);
        }

        [Fact]
        public async Task Create_Post_WithReturnUrl_RedirectsToReturnUrl()
        {
            var controller = GetController();

            var model = new GenreFormModel { Name = "New Genre" };

            var result = await controller.Create(model, "/Admin/Books");

            var redirect = Assert.IsType<RedirectResult>(result);
            Assert.Equal("/Admin/Books", redirect.Url);
        }

        // ---------------------------------------------------------
        // EDIT GET
        // ---------------------------------------------------------
        [Fact]
        public async Task Edit_Get_ReturnsNotFound_WhenMissing()
        {
            var controller = GetController();

            _genreService.Setup(s => s.GetByIdAsync(1))
                .ReturnsAsync((GenreFormModel?)null);

            var result = await controller.Edit(1);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Edit_Get_ReturnsViewWithModel()
        {
            var controller = GetController();

            _genreService.Setup(s => s.GetByIdAsync(1))
                .ReturnsAsync(new GenreFormModel { Name = "Fantasy" });

            var result = await controller.Edit(1);

            var view = Assert.IsType<ViewResult>(result);
            Assert.IsType<GenreFormModel>(view.Model);
        }

        // ---------------------------------------------------------
        // EDIT POST
        // ---------------------------------------------------------
        [Fact]
        public async Task Edit_Post_InvalidModel_ReturnsView()
        {
            var controller = GetController();
            controller.ModelState.AddModelError("Name", "Required");

            var model = new GenreFormModel();

            var result = await controller.Edit(1, model);

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task Edit_Post_Valid_RedirectsToIndex()
        {
            var controller = GetController();

            var model = new GenreFormModel { Name = "Updated" };

            var result = await controller.Edit(1, model);

            Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", ((RedirectToActionResult)result).ActionName);
        }

        // ---------------------------------------------------------
        // DELETE GET
        // ---------------------------------------------------------
        [Fact]
        public async Task Delete_Get_ReturnsNotFound_WhenMissing()
        {
            var controller = GetController();

            _genreService.Setup(s => s.GetByIdAsync(1))
                .ReturnsAsync((GenreFormModel?)null);

            var result = await controller.Delete(1);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Delete_Get_ReturnsView()
        {
            var controller = GetController();

            _genreService.Setup(s => s.GetByIdAsync(1))
                .ReturnsAsync(new GenreFormModel { Name = "Fantasy" });

            var result = await controller.Delete(1);

            Assert.IsType<ViewResult>(result);
        }

        // ---------------------------------------------------------
        // DELETE POST
        // ---------------------------------------------------------
        [Fact]
        public async Task DeleteConfirmed_RedirectsToIndex()
        {
            var controller = GetController();

            var result = await controller.DeleteConfirmed(1);

            Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", ((RedirectToActionResult)result).ActionName);
        }
    }
}
