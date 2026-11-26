namespace LibrarySystem.BLL.Services
{
    public sealed class BookService : IBookService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BookService(IUnitOfWork unitOfWork)
            => _unitOfWork = unitOfWork;

        public async Task<Result<Book>> CreateAsync(Book book)
        {
            await _unitOfWork.Books.CreateAsync(book);
            await _unitOfWork.SaveChangesAsync();
            return Result<Book>.Success(book);
        }

        public async Task<Result<Book>> DeleteAsync(int id)
        {
            var result = await GetByIdAsync(id);

            if (!result.Successeded || result.Data is null)
                return result;

            _unitOfWork.Books.Delete(result.Data);
            await _unitOfWork.SaveChangesAsync();
            return result;
        }

        public async Task<Result<PaginatedList<Book>>> GetAllAsync(string? searchValue, int pageIndex, int pageSize)
        {
            var books = string.IsNullOrWhiteSpace(searchValue)
                            ? await _unitOfWork.Books.GetAllWithAuthorAsync()
                            : await _unitOfWork.Books.GetAllAsync(searchValue);

            var paginatedList = PaginatedList<Book>.Create(books, pageIndex, pageSize);
            return Result<PaginatedList<Book>>.Success(paginatedList);
        }

        public async Task<Result<Book>> GetByIdAsync(int id)
        {
            var book = await _unitOfWork.Books.GetAsync(id);

            return book is null
                ? Result<Book>.Failure("Book is not found.")
                : Result<Book>.Success(book);
        }

        public async Task<Result<Book>> UpdateAsync(Book book)
        {
            _unitOfWork.Books.Update(book);
            await _unitOfWork.SaveChangesAsync();
            return Result<Book>.Success(book);
        }
    }
}