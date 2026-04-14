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

            foreach (var book in books)
            {
                book.DetailsUrl = Url.Action("Details", "Books", new { area = "Admin", id = book.Id }) ?? "#";
            }

            return View(books);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);

            if (book == null)
                return NotFound();

            var vm = new BookDetailsViewModel
            {
                Id = book.Id,
                Title = book.Title,
                Description = book.Description ?? "No description available",
                ISBN = book.ISBN,
                Year = book.Year,
                CopiesAvailable = book.CopiesAvailable,
                CoverImagePath = book.CoverImagePath ?? "/images/no-cover.png",

                AuthorName = book.Author.Name,
                AuthorBiography = book.Author?.Biography ?? "No biography available",

                GenreName = book.Genre.Name
            };

            return View(vm);
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var vm = new BookFormViewModel
            {

                Year = 2000,
                CopiesAvailable = 1,

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

            return View("CreateAndEdit",vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookFormViewModel model)
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

                return View("CreateAndEdit", model);
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


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);

            if (book == null)
                return NotFound();

            var vm = new BookFormViewModel
            {
                Title = book.Title,
                Description = book.Description ?? "No description available",
                ISBN = book.ISBN,
                CoverImagePath = book.CoverImagePath ?? "/images/no-cover.png",
                Year = book.Year,
                CopiesAvailable = book.CopiesAvailable,
                AuthorId = book.AuthorId,
                GenreId = book.GenreId,

                Authors = (await _authorService.GetAllAsync())
                    .Select(a => new SelectListItem
                    {
                        Value = a.Id.ToString(),
                        Text = a.Name,
                        Selected = a.Id == book.AuthorId
                    }),

                Genres = (await _genreService.GetAllAsync())
                    .Select(g => new SelectListItem
                    {
                        Value = g.Id.ToString(),
                        Text = g.Name,
                        Selected = g.Id == book.GenreId
                    })
            };

            return View("CreateAndEdit", vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BookFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Authors = (await _authorService.GetAllAsync())
                    .Select(a => new SelectListItem
                    {
                        Value = a.Id.ToString(),
                        Text = a.Name
                    });

                model.Genres = (await _genreService.GetAllAsync())
                    .Select(g => new SelectListItem
                    {
                        Value = g.Id.ToString(),
                        Text = g.Name
                    });

                return View("CreateAndEdit", model);
            }

            var book = await _bookService.GetBookByIdAsync(id);

            if (book == null)
                return NotFound();

            book.Title = model.Title;
            book.Description = model.Description;
            book.ISBN = model.ISBN;
            book.CoverImagePath = model.CoverImagePath;
            book.Year = model.Year;
            book.CopiesAvailable = model.CopiesAvailable;
            book.AuthorId = model.AuthorId;
            book.GenreId = model.GenreId;

            await _bookService.UpdateBookAsync(book);

            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);

            if (book == null)
                return NotFound();

            var vm = new BookDeleteViewModel
            {
                Id = book.Id,
                Title = book.Title,
                Year = book.Year,
                AuthorName = book.Author.Name
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(BookDeleteViewModel model)
        {
            var book = await _bookService.GetBookByIdAsync(model.Id);

            if (book == null)
                return NotFound();

            await _bookService.DeleteBookAsync(book);

            return RedirectToAction("Index");
        }

    }
}
