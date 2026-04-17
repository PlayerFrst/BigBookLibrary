using BigBookLibrary.Controllers;
using BigBookLibrary.Services;
using BigBookLibrary.Services.Interfaces;
using BigBookLibrary.ViewModels.Reviews;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using System.Security.Claims;

namespace BigBookLibrary.Tests.Controllers
{
    public class ReviewControllerTests
    {
        private Mock<IReviewService> _reviewService;
        private Mock<IBookService> _bookService;
        private ReviewController GetController(string userId = "user1", bool isAdmin = false)
        {
            _reviewService = new Mock<IReviewService>();
            _bookService = new Mock<IBookService>();

            var controller = new ReviewController(_reviewService.Object, _bookService.Object);

            // Mock User
            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, userId)
    };

            if (isAdmin)
                claims.Add(new Claim(ClaimTypes.Role, "Administrator"));

            var identity = new ClaimsIdentity(claims, "TestAuth");
            var user = new ClaimsPrincipal(identity);

            var httpContext = new DefaultHttpContext { User = user };

            var tempDataProvider = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());
            controller.TempData = tempDataProvider;

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };

            return controller;
        }


        // ---------------------------------------------------------
        // ADD REVIEW
        // ---------------------------------------------------------
        [Fact]
        public async Task Add_InvalidModel_RedirectsWithError()
        {
            var controller = GetController();
            controller.ModelState.AddModelError("Rating", "Required");

            var model = new ReviewFormModel();

            var result = await controller.Add(5, model);

            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Details", redirect.ActionName);
            Assert.Equal("Books", redirect.ControllerName);

            Assert.Equal("Please fill in the review correctly.", controller.TempData["ReviewError"]);
        }

        [Fact]
        public async Task Add_ValidModel_AddsReviewAndRedirects()
        {
            var controller = GetController();

            var model = new ReviewFormModel
            {
                Rating = 5,
                Comment = "Nice!"
            };

            var result = await controller.Add(10, model);

            _reviewService.Verify(s => s.AddReviewAsync(10, "user1", model), Times.Once);

            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Details", redirect.ActionName);
            Assert.Equal("Books", redirect.ControllerName);

            Assert.Equal("Your review has been submitted successfully.", controller.TempData["ReviewSuccess"]);
        }

        // ---------------------------------------------------------
        // DELETE REVIEW
        // ---------------------------------------------------------
        [Fact]
        public async Task Delete_UserNotOwnerAndNotAdmin_ReturnsError()
        {
            var controller = GetController(userId: "user1");

            _reviewService.Setup(s => s.CanUserDeleteReviewAsync(1, "user1"))
                .ReturnsAsync(false);

            var result = await controller.Delete(1, 5);

            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Details", redirect.ActionName);

            Assert.Equal("You do not have permission to delete this review.", controller.TempData["ReviewError"]);
        }

        [Fact]
        public async Task Delete_UserIsOwner_DeletesReview()
        {
            var controller = GetController(userId: "user1");

            _reviewService.Setup(s => s.CanUserDeleteReviewAsync(1, "user1"))
                .ReturnsAsync(true);

            var result = await controller.Delete(1, 5);

            _reviewService.Verify(s => s.DeleteReviewAsync(1), Times.Once);

            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Details", redirect.ActionName);

            Assert.Equal("The review has been deleted successfully.", controller.TempData["ReviewSuccess"]);
        }

        [Fact]
        public async Task Delete_AdminCanDeleteEvenIfNotOwner()
        {
            var controller = GetController(userId: "user1", isAdmin: true);

            _reviewService.Setup(s => s.CanUserDeleteReviewAsync(1, "user1"))
                .ReturnsAsync(false);

            var result = await controller.Delete(1, 5);

            _reviewService.Verify(s => s.DeleteReviewAsync(1), Times.Once);

            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Details", redirect.ActionName);

            Assert.Equal("The review has been deleted successfully.", controller.TempData["ReviewSuccess"]);
        }
    }
}
