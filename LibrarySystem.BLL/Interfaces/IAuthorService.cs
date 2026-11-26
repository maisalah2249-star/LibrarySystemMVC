namespace LibrarySystem.BLL.Interfaces
{
    public interface IAuthorService
    {
        Task<Result<PaginatedList<Author>>> GetAllAsync(int pageIndex, int pageSize);
        Task<Result<Author>> GetByIdAsync(int id);
        Task<Result<Author>> CreateAsync(Author author);
        Task<Result<Author>> UpdateAsync(Author author);
        Task<Result<Author>> DeleteAsync(int id);
    }
}