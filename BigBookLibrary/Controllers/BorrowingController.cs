using BigBookLibrary.Extensions;
using BigBookLibrary.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BigBookLibrary.Controllers
{
    [Authorize]
    public class BorrowingController : Controller
    {
        private readonly IBorrowingService _borrowingService;
        private readonly IBookService _bookService;

        public BorrowingController(IBorrowingService borrowingService,
            IBookService bookService)
        {
            _borrowingService = borrowingService;
            _bookService = bookService;
        }

        public async Task<IActionResult> Rent(int id)
        {
            var userId = User.GetId();

            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null)
                return NotFound();

            if (!await _borrowingService.IsBookAvailable(id))
            {
                TempData["Error"] = "No copies available for this book.";
                return RedirectToAction("Details", "Book", new { id });
            }

            var success = await _borrowingService.RentBookAsync(id, userId);

            if (!success)
            {
                TempData["Error"] = "Unable to rent this book.";
                return RedirectToAction("Details", "Book", new { id });
            }

            TempData["Success"] = "Book rented successfully!";
            return RedirectToAction("MyBorrowings");
        }

        public async Task<IActionResult> Return(int id)
        {
            var success = await _borrowingService.ReturnBookAsync(id);

            if (!success)
            {
                TempData["Error"] = "Unable to return this book.";
            }
            else
            {
                TempData["Success"] = "Book returned successfully!";
            }

            return RedirectToAction("MyBorrowings");
        }

        public async Task<IActionResult> MyBorrowings()
        {
            var userId = User.GetId();

            var borrowings = await _borrowingService.GetActiveBorrowingsForUser(userId);

            return View(borrowings);
        }


    }
}
