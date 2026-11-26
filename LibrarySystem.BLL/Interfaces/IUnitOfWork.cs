namespace LibrarySystem.BLL.Interfaces
{
    public interface IUnitOfWork
    {
        IAuthorRepository Authors { get; }
        IBookRepository Books { get; }
        Task<int> SaveChangesAsync();
    }
}