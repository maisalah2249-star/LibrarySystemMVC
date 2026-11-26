namespace LibrarySystem.BLL.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;

        private readonly Lazy<IAuthorRepository> _authorRepository;
        private readonly Lazy<IBookRepository> _bookRepository;

        public IAuthorRepository Authors => _authorRepository.Value;
        public IBookRepository Books => _bookRepository.Value;

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _authorRepository = new Lazy<IAuthorRepository>(() => new AuthorRepository(_dbContext));
            _bookRepository = new Lazy<IBookRepository>(() => new BookRepository(_dbContext));
        }

        public async Task<int> SaveChangesAsync()
            => await _dbContext.SaveChangesAsync();
    }
}