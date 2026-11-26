using AutoMapper;
using LibrarySystem.PL.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LibrarySystem.PL.Controllers
{
    public class BooksController : Controller
    {
        private const int PageSize = 10;
        private readonly IBookService _bookService;
        private readonly IAuthorService _authorService;
        private readonly IMapper _mapper;

        public BooksController(IBookService bookService, IAuthorService authorService, IMapper mapper)
        {
            _bookService = bookService;
            _authorService = authorService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string? searchValue, int pageIndex = 1)
        {
            var result = await _bookService.GetAllAsync(searchValue, pageIndex, PageSize);

            if (!result.Successeded || result.Data is null)
                return Problem(result.Error);

            var books = _mapper.Map<IEnumerable<BookViewModel>>(result.Data);

            var model = PaginatedList<BookViewModel>.Create(books, result.Data.TotalCount, result.Data.PageIndex, PageSize);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await LoadAuthorsAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(BookViewModel bookVM)
        {
            if (bookVM.AuthorId is null)
                ModelState.AddModelError(nameof(BookViewModel.AuthorId), "Please select an author.");

            if (!ModelState.IsValid)
            {
                await LoadAuthorsAsync();
                return View(bookVM);
            }

            if (bookVM.Image is not null)
                bookVM.ImageName = DocumentSettings.UploadFile(bookVM.Image, "Images");

            var book = _mapper.Map<BookViewModel, Book>(bookVM);

            var result = await _bookService.CreateAsync(book);
            if (!result.Successeded)
            {
                ModelState.AddModelError(string.Empty, result.Error!);
                await LoadAuthorsAsync();
                return View(bookVM);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
            => await BookControllerHandlerAsync(id, nameof(Details));

        public async Task<IActionResult> Edit(int? id)
            => await BookControllerHandlerAsync(id, nameof(Edit));

        [HttpPost]
        public async Task<IActionResult> Edit([FromRoute] int id, BookViewModel bookVM)
        {
            if (id != bookVM.Id)
                return BadRequest();

            if (bookVM.AuthorId is null)
                ModelState.AddModelError(nameof(BookViewModel.AuthorId), "Please select an author.");

            if (ModelState.IsValid)
            {
                if (bookVM.Image is not null)
                {
                    // ImageName القديم جاي من الـ hidden input، فمش محتاجين رحلة زيادة للداتابيز
                    if (!string.IsNullOrEmpty(bookVM.ImageName))
                        DocumentSettings.DeleteFile(bookVM.ImageName, "Images");

                    bookVM.ImageName = DocumentSettings.UploadFile(bookVM.Image, "Images");
                }

                var book = _mapper.Map<BookViewModel, Book>(bookVM);
                var result = await _bookService.UpdateAsync(book);

                if (!result.Successeded)
                    ModelState.AddModelError(string.Empty, result.Error!);
                else
                    return RedirectToAction(nameof(Index));
            }

            await LoadAuthorsAsync();
            return View(bookVM);
        }

        public async Task<IActionResult> Delete(int? id)
            => await BookControllerHandlerAsync(id, nameof(Delete));

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int? id)
        {
            if (!id.HasValue)
                return BadRequest();

            // Soft delete: السجل بيتعلّم IsDeleted فقط، فبنسيب الصورة موجودة
            var result = await _bookService.DeleteAsync(id.Value);

            if (!result.Successeded || result.Data is null)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }

        private async Task LoadAuthorsAsync()
        {
            var authors = (await _authorService.GetAllAsync(1, int.MaxValue)).Data
                          ?? PaginatedList<Author>.Create(Enumerable.Empty<Author>(), 1, 1);

            ViewBag.Authors = new SelectList(authors, "Id", "Name");
        }

        private async Task<IActionResult> BookControllerHandlerAsync(int? id, string viewName)
        {
            if (!id.HasValue)
                return BadRequest();

            var result = await _bookService.GetByIdAsync(id.Value);

            if (!result.Successeded || result.Data is null)
                return NotFound();

            if (viewName == nameof(Edit))
                await LoadAuthorsAsync();

            var bookVM = _mapper.Map<BookViewModel>(result.Data);

            return View(viewName, bookVM);
        }
    }
}