using BigBookLibrary.Services;
using BigBookLibrary.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BigBookLibrary.Controllers
{
    public class AuthorController : Controller
    {

        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }


        public async Task<IActionResult> Index()
        {
            var authors = await _authorService.GetAllAsync();
            return View(authors);
        }

        public async Task<IActionResult> Books(int id)
        {
            var author = await _authorService.GetAuthorEntityByIdAsync(id);

            if (author == null)
            {
                return NotFound();
            }

            var books = await _authorService.GetBooksByAuthorAsync(id);

            foreach (var book in books)
            {
                book.DetailsUrl = Url.Action("Details", "Books", new { id = book.Id }) ?? "#";
            }

            ViewBag.AuthorName = author.Name;

            return View(books);
        }
    }
}
