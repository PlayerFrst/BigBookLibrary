using BigBookLibrary.Areas.Admin.ViewModels.Books;
using BigBookLibrary.Areas.Admin.ViewModels.Shared;
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

        public async Task<IActionResult> Index(int page = 1, string? search = null, string sort = "newest")
        {
            const int pageSize = 8;

            var booksQuery = (await _bookService.GetAllBooksAsync()).AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                booksQuery = booksQuery
                    .Where(b => b.Title.Contains(search, StringComparison.OrdinalIgnoreCase));
            }

            switch (sort)
            {
                case "title_asc":
                    booksQuery = booksQuery.OrderBy(b => b.Title);
                    break;

                case "title_desc":
                    booksQuery = booksQuery.OrderByDescending(b => b.Title);
                    break;

                case "oldest":
                    booksQuery = booksQuery.OrderBy(b => b.Id);
                    break;

                default: // newest
                    booksQuery = booksQuery.OrderByDescending(b => b.Id);
                    break;
            }

            var totalBooks = booksQuery.Count();

            var books = booksQuery
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            foreach (var book in books)
            {
                book.DetailsUrl = Url.Action("Details", "Books", new { id = book.Id }) ?? "#";
            }

            var viewModel = new BooksListViewModel
            {
                Books = books,
                Pagination = new PaginationViewModel
                {
                    CurrentPage = page,
                    TotalPages = (int)Math.Ceiling(totalBooks / (double)pageSize)
                }
            };

            return View(viewModel);
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
