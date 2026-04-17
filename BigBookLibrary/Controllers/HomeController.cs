using BigBookLibrary.Models;
using BigBookLibrary.Services.Interfaces;
using BigBookLibrary.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BigBookLibrary.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBookService _bookService;

        public HomeController(IBookService bookService, ILogger<HomeController> logger)
        {
            _bookService = bookService;
            _logger = logger;
        }


        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Books");
            }

            var popularBooks = await _bookService.GetPopularBooksAsync(4);

            foreach (var book in popularBooks)
            {
                book.DetailsUrl = Url.Action("Details", "Books", new { id = book.Id }) ?? "#";
            }

            var model = new HomeViewModel
            {
                PopularBooks = popularBooks
            };

            return View(model);
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
