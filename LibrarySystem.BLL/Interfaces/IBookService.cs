namespace LibrarySystem.BLL.Interfaces
{
    public interface IBookService
    {
        Task<Result<PaginatedList<Book>>> GetAllAsync(string? searchValue, int pageIndex, int pageSize);
        Task<Result<Book>> GetByIdAsync(int id);
        Task<Result<Book>> CreateAsync(Book book);
        Task<Result<Book>> UpdateAsync(Book book);
        Task<Result<Book>> DeleteAsync(int id);
    }
}