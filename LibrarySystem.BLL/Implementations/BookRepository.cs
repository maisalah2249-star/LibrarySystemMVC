namespace LibrarySystem.BLL.Implementations
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        public BookRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Book>> GetAllAsync(string title)
            => await _dbSet.Include(b => b.Author)
                           .Where(b => b.Title.ToLower().Contains(title.ToLower()))
                           .ToListAsync();

        public async Task<IEnumerable<Book>> GetAllWithAuthorAsync()
            => await _dbSet.Include(b => b.Author).ToListAsync();

        public override async Task<Book?> GetAsync(int id)
            => await _dbSet.Include(b => b.Author).FirstOrDefaultAsync(b => b.Id == id);
    }
}