using Microsoft.AspNetCore.Mvc;

namespace LibrarySystem.PL.Controllers
{
    public class AuthorsController : Controller
    {
        private const int PageSize = 10;
        private readonly IAuthorService _authorService;

        public AuthorsController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int pageIndex = 1)
        {
            var result = await _authorService.GetAllAsync(pageIndex, PageSize);

            if (!result.Successeded || result.Data is null)
                return Problem(result.Error);

            return View(result.Data);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Author author)
        {
            if (!ModelState.IsValid)
                return View(author);

            var result = await _authorService.CreateAsync(author);

            if (!result.Successeded)
            {
                ModelState.AddModelError(string.Empty, result.Error!);
                return View(author);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
            => await AuthorControllerHandlerAsync(id, nameof(Details));

        public async Task<IActionResult> Edit(int? id)
            => await AuthorControllerHandlerAsync(id, nameof(Edit));

        [HttpPost]
        public async Task<IActionResult> Edit([FromRoute] int id, Author author)
        {
            if (id != author.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                var result = await _authorService.UpdateAsync(author);

                if (!result.Successeded)
                    ModelState.AddModelError(string.Empty, result.Error!);
                else
                    return RedirectToAction(nameof(Index));
            }

            return View(author);
        }

        public async Task<IActionResult> Delete(int? id)
            => await AuthorControllerHandlerAsync(id, nameof(Delete));

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int? id)
        {
            if (!id.HasValue)
                return BadRequest();

            var result = await _authorService.DeleteAsync(id.Value);
            if (!result.Successeded || result.Data is null)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }

        private async Task<IActionResult> AuthorControllerHandlerAsync(int? id, string viewName)
        {
            if (!id.HasValue)
                return BadRequest();

            var result = await _authorService.GetByIdAsync(id.Value);

            if (!result.Successeded || result.Data is null)
                return NotFound();

            return View(viewName, result.Data);
        }
    }
}