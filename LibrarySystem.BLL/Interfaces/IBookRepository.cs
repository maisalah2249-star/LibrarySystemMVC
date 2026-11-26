namespace LibrarySystem.BLL.Interfaces
{
    public interface IBookRepository : IGenericRepository<Book>
    {
        Task<IEnumerable<Book>> GetAllAsync(string title);
        Task<IEnumerable<Book>> GetAllWithAuthorAsync();
    }
}