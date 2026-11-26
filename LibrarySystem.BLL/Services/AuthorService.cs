namespace LibrarySystem.BLL.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AuthorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Author>> CreateAsync(Author author)
        {
            await _unitOfWork.Authors.CreateAsync(author);
            await _unitOfWork.SaveChangesAsync();
            return Result<Author>.Success(author);
        }

        public async Task<Result<Author>> DeleteAsync(int id)
        {
            var result = await GetByIdAsync(id);

            if (!result.Successeded || result.Data is null)
                return result;

            _unitOfWork.Authors.Delete(result.Data);
            await _unitOfWork.SaveChangesAsync();
            return result;
        }

        public async Task<Result<PaginatedList<Author>>> GetAllAsync(int pageIndex, int pageSize)
        {
            var authors = await _unitOfWork.Authors.GetAllAsync();
            var paginatedList = PaginatedList<Author>.Create(authors, pageIndex, pageSize);

            return Result<PaginatedList<Author>>.Success(paginatedList);
        }

        public async Task<Result<Author>> GetByIdAsync(int id)
        {
            var author = await _unitOfWork.Authors.GetAsync(id);

            return author is null
                ? Result<Author>.Failure("Author is not found.")
                : Result<Author>.Success(author);
        }

        public async Task<Result<Author>> UpdateAsync(Author author)
        {
            _unitOfWork.Authors.Update(author);
            await _unitOfWork.SaveChangesAsync();
            return Result<Author>.Success(author);
        }
    }
}