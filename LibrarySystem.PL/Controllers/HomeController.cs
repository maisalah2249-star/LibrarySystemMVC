using LibrarySystem.PL.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LibrarySystem.PL.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View();

        public IActionResult Privacy() => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
            => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult ServerError()
        {
            Response.StatusCode = StatusCodes.Status500InternalServerError;
            return View("InternalServerError");
        }

        [ActionName("StatusCode")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult HandleStatusCode(int code)
        {
            if (code == StatusCodes.Status404NotFound)
            {
                Response.StatusCode = code;
                return View("NotFound");
            }

            if (code >= StatusCodes.Status500InternalServerError)
            {
                Response.StatusCode = StatusCodes.Status500InternalServerError;
                return View("InternalServerError");
            }

            return View("NotFound");
        }
    }
}