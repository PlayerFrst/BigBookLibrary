using BigBookLibrary.Areas.Admin.ViewModels.Genres;
using BigBookLibrary.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BigBookLibrary.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Administrator")]
    public class GenresController : Controller
    {
        private readonly IGenreService genreService;

        public GenresController(IGenreService genreService)
        {
            this.genreService = genreService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var genres = await genreService.GetAllAsync();
            return View(genres);
        }

        [HttpGet]
        public IActionResult Create(string? returnUrl = null)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View(new GenreFormModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GenreFormModel model, string? returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ReturnUrl = returnUrl;
                return View(model);
            }

            await genreService.CreateAsync(model);

            if (!string.IsNullOrEmpty(returnUrl))
                return Redirect(returnUrl);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await genreService.GetByIdAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, GenreFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await genreService.EditAsync(id, model);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var genre = await genreService.GetByIdAsync(id);

            if (genre == null)
            {
                return NotFound();
            }

            return View(genre);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await genreService.SoftDeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
