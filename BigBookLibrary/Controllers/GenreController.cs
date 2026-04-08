using BigBookLibrary.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BigBookLibrary.Controllers
{
    public class GenreController : Controller
    {
        private readonly IGenreService _genreService;

        public GenreController(IGenreService genreService)
        {
            _genreService = genreService;
        }
        public async Task<IActionResult> Index()
        {
            var genres = await _genreService.GetAllAsync();
            return View(genres);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Books(int id)
        {
            var books = await _genreService.GetBooksByGenreAsync(id);

            foreach (var book in books)
            {
                book.DetailsUrl = Url.Action("Details", "Books", new { id = book.Id }) ?? "#";
            }

            return View(books);
        }
    }
}
