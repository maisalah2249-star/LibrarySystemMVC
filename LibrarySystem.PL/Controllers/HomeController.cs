using LibrarySystem.PL.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LibrarySystem.PL.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View();

        public IActionResult Privacy() => View();

        public IActionResult ServerError() => View("InternalServerError");

        [ActionName("StatusCode")]
        public IActionResult HandleStatusCode(int code)
            => code == 404 ? View("NotFound") : View("InternalServerError");

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
            => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}