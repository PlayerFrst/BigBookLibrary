using BigBookLibrary.Services.Interfaces;
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


        //public async Task<IActionResult> Search(string query)
        //{
        //    var results = await _bookService.SearchAsync(query);
        //    return View(results);
        //}
    }
}
