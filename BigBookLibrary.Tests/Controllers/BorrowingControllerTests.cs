using BigBookLibrary.Controllers;
using BigBookLibrary.Models;
using BigBookLibrary.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using System.Security.Claims;

namespace BigBookLibrary.Tests.Controllers
{
    public class BorrowingControllerTests
    {
        private Mock<IBorrowingService> _borrowingService;
        private Mock<IBookService> _bookService;

        private BorrowingController GetController(
            string userId = "user1",
            bool isAdmin = false)
        {
            _borrowingService = new Mock<IBorrowingService>();
            _bookService = new Mock<IBookService>();

            var controller = new BorrowingController(
                _borrowingService.Object,
                _bookService.Object);

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

            // TempData
            controller.TempData = new TempDataDictionary(
                httpContext,
                Mock.Of<ITempDataProvider>());

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };

            return controller;
        }

        // ---------------------------------------------------------
        // RENT
        // ---------------------------------------------------------
        [Fact]
        public async Task Rent_ReturnsNotFound_WhenBookMissing()
        {
            var controller = GetController();

            _bookService.Setup(s => s.GetBookByIdAsync(1))
                .ReturnsAsync((Book?)null);

            var result = await controller.Rent(1);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Rent_UserHasOverdueBooks_ShowsError()
        {
            var controller = GetController();

            _bookService.Setup(s => s.GetBookByIdAsync(1))
                .ReturnsAsync(new Book());

            _borrowingService.Setup(s => s.UserHasOverdueBooks("user1"))
                .ReturnsAsync(true);

            var result = await controller.Rent(1);

            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Details", redirect.ActionName);

            Assert.Equal(
                "You have overdue books. Please return them before borrowing new ones.",
                controller.TempData["Error"]);
        }

        [Fact]
        public async Task Rent_UserAlreadyBorrowed_ShowsError()
        {
            var controller = GetController();

            _bookService.Setup(s => s.GetBookByIdAsync(1))
                .ReturnsAsync(new Book());

            _borrowingService.Setup(s => s.UserHasOverdueBooks("user1"))
                .ReturnsAsync(false);

            _borrowingService.Setup(s => s.UserAlreadyBorrowedBook("user1", 1))
                .ReturnsAsync(true);

            var result = await controller.Rent(1);

            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Details", redirect.ActionName);

            Assert.Equal("You have already borrowed this book.", controller.TempData["Error"]);
        }

        [Fact]
        public async Task Rent_SuccessfulBorrow_SetsSuccessMessage()
        {
            var controller = GetController();

            _bookService.Setup(s => s.GetBookByIdAsync(1))
                .ReturnsAsync(new Book());

            _borrowingService.Setup(s => s.UserHasOverdueBooks("user1"))
                .ReturnsAsync(false);

            _borrowingService.Setup(s => s.UserAlreadyBorrowedBook("user1", 1))
                .ReturnsAsync(false);

            _borrowingService.Setup(s => s.RentBookAsync(1, "user1"))
                .ReturnsAsync(true);

            var result = await controller.Rent(1);

            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Details", redirect.ActionName);

            Assert.Equal("Book successfully borrowed!", controller.TempData["BorrowSuccess"]);
        }

        // ---------------------------------------------------------
        // RETURN
        // ---------------------------------------------------------
        [Fact]
        public async Task Return_Failure_ShowsError()
        {
            var controller = GetController();

            _borrowingService.Setup(s => s.ReturnBookAsync(1))
                .ReturnsAsync(false);

            var result = await controller.Return(1);

            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("MyBorrowings", redirect.ActionName);

            Assert.Equal("Unable to return this book.", controller.TempData["Error"]);
        }

        [Fact]
        public async Task Return_Success_ShowsSuccessMessage()
        {
            var controller = GetController();

            _borrowingService.Setup(s => s.ReturnBookAsync(1))
                .ReturnsAsync(true);

            var result = await controller.Return(1);

            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("MyBorrowings", redirect.ActionName);

            Assert.Equal("Book returned successfully!", controller.TempData["Success"]);
        }

        // ---------------------------------------------------------
        // MY BORROWINGS
        // ---------------------------------------------------------
        [Fact]
        public async Task MyBorrowings_ReturnsViewWithBorrowings()
        {
            var controller = GetController();

            _borrowingService.Setup(s => s.GetActiveBorrowingsForUser("user1"))
                .ReturnsAsync(new List<Borrowing>
                {
                    new Borrowing { Id = 1 }
                });

            var result = await controller.MyBorrowings();

            var view = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Borrowing>>(view.Model);

            Assert.Single(model);
        }
    }
}
