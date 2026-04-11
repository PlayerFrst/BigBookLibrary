using Microsoft.AspNetCore.Mvc;

namespace BigBookLibrary.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/404")]
        public IActionResult Error404()
        {
            return View("~/Views/Shared/404.cshtml");
        }

        [Route("Error/500")]
        public IActionResult Error500()
        {
            return View("~/Views/Shared/500.cshtml");
        }
    }
}
