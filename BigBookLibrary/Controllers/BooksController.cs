using BigBookLibrary.Models;
using BigBookLibrary.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BigBookLibrary.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        public async Task<IActionResult> Index()
        {
            var books = await _bookService.GetAllBooksAsync();
            foreach (var book in books)
            {
                book.DetailsUrl = Url.Action("Details", "Books", new { id = book.Id }) ?? "#";
            }
            return View(books);
        }

        public async Task<IActionResult> Details(int id)
        {
            var model = await _bookService.GetBookDetailsAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }


        [AllowAnonymous]
        public async Task<IActionResult> Search(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return View(new List<Book>());
            }

            var results = await _bookService.SearchBooksAsync(query);

            return View(results);
        }

    }
}
