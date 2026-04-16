using BigBookLibrary.ViewModels.Books;
using BigBookLibrary.Extensions;
using BigBookLibrary.Services;
using BigBookLibrary.Services.Interfaces;
using BigBookLibrary.ViewModels.Reviews;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BigBookLibrary.Controllers
{
    [Authorize] 
    public class ReviewController : Controller
    {
        private readonly IReviewService _reviewService;
        private readonly IBookService _bookService;

        public ReviewController(IReviewService reviewService, IBookService bookService)
        {
            _reviewService = reviewService;
            _bookService = bookService;
        }

        [HttpPost]
        public async Task<IActionResult> Add(int bookId, ReviewFormModel model)
        {
            if (!ModelState.IsValid)
            {
                var book = await _bookService.GetBookDetailsAsync(bookId);

                var viewModel = new BookDetailsViewModel
                {
                    Id = book.Id,
                    Title = book.Title,
                    AuthorName = book.AuthorName,
                    GenreName = book.GenreName,
                    Description = book.Description,
                    CoverImagePath = book.CoverImagePath,
                    CopiesAvailable = book.CopiesAvailable,
                    Reviews = await _reviewService.GetReviewsForBookAsync(bookId),
                    ReviewForm = model
                };

                return View("~/Views/Books/Details.cshtml", viewModel);
            }


            var userId = User.GetId();

            await _reviewService.AddReviewAsync(bookId, userId, model);

            TempData["ReviewSuccess"] = "Your review has been submitted successfully.";

            return RedirectToAction("Details", "Books", new { id = bookId });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, int bookId)
        {
            var userId = User.GetId();

            var canDelete = await _reviewService.CanUserDeleteReviewAsync(id, userId)
                           || User.IsInRole("Administrator");

            if (!canDelete)
            {
                TempData["ReviewError"] = "You do not have permission to delete this review.";
                return RedirectToAction("Details", "Books", new { id = bookId });
            }

            await _reviewService.DeleteReviewAsync(id);

            TempData["ReviewSuccess"] = "The review has been deleted successfully.";

            return RedirectToAction("Details", "Books", new { id = bookId });
        }
    }
}
