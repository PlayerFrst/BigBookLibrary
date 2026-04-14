using BigBookLibrary.Areas.Admin.ViewModels.Authors;
using BigBookLibrary.Services;
using BigBookLibrary.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BigBookLibrary.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Administrator")]
    public class AuthorsController : Controller
    {
        private readonly IAuthorService _authorService;

        public AuthorsController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        
        public async Task<IActionResult> Index()
        {
            var authors = await _authorService.GetAllAsync();
            return View(authors);
        }

        [HttpGet]
        public IActionResult Create(string? returnUrl = null)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View(new AuthorFormModel());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AuthorFormModel model, string? returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ReturnUrl = returnUrl;
                return View(model);
            }

            await _authorService.CreateAsync(model);

            if (!string.IsNullOrEmpty(returnUrl))
                return Redirect(returnUrl);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await _authorService.GetByIdAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AuthorFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _authorService.EditAsync(id, model);
            return RedirectToAction(nameof(Index));
        }

        
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _authorService.SoftDeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
