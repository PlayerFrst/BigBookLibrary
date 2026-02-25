using BigBookLibrary.Areas.Admin.ViewModels.Books;
using BigBookLibrary.Models;
using BigBookLibrary.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BigBookLibrary.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Administrator")]
    public class BooksController : Controller
    {

        private readonly IBookService _bookService;
        private readonly IAuthorService _authorService;
        private readonly IGenreService _genreService;

        public BooksController(IBookService bookService,IAuthorService authorService, IGenreService genreService)
        {
            _bookService = bookService;
            _authorService = authorService;
            _genreService = genreService;
        }

        public async Task<IActionResult> Index(string? search)
        {
            var books = await _bookService.GetAllBooksAsync();

            if (!string.IsNullOrWhiteSpace(search))
            {
                books = books
                    .Where(b => b.Title.Contains(search, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            return View(books);
        }

        public async Task<IActionResult> Details(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);

            if (book == null)
                return NotFound();

            return View(book);
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var vm = new BookCreateViewModel
            {
                Authors = (await _authorService.GetAllAsync())
                    .Select(a => new SelectListItem
                    {
                        Value = a.Id.ToString(),
                        Text = a.Name
                    }),

                Genres = (await _genreService.GetAllAsync())
                    .Select(g => new SelectListItem
                    {
                        Value = g.Id.ToString(),
                        Text = g.Name
                    })
            };

            return View(vm);
        }




        [HttpPost]
        public async Task<IActionResult> Create(BookCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Authors = (await _authorService.GetAllAsync())
                    .Select(a => new SelectListItem
                    {
                        Value = a.Id.ToString(),
                        Text = a.Name
                    })
                    .ToList();

                model.Genres = (await _genreService.GetAllAsync())
                    .Select(g => new SelectListItem
                    {
                        Value = g.Id.ToString(),
                        Text = g.Name
                    })
                    .ToList();

                return View(model);
            }

            var book = new Book
            {
                Title = model.Title,
                Description = model.Description,
                ISBN = model.ISBN,
                CoverImagePath = model.CoverImagePath,
                Year = model.Year,
                CopiesAvailable = model.CopiesAvailable,
                AuthorId = model.AuthorId,
                GenreId = model.GenreId
            };

            await _bookService.CreateBookAsync(book);

            return RedirectToAction("Index");
        }






    }
}
