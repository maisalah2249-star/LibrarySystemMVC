namespace LibrarySystem.BLL.Implementations
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly ApplicationDbContext _dbContext;
        protected DbSet<TEntity> _dbSet;

        public GenericRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }

        public async Task CreateAsync(TEntity entity)
            => await _dbSet.AddAsync(entity);

        public void Delete(TEntity entity)
        {
            entity.IsDeleted = true;
            _dbSet.Update(entity);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
            => await _dbSet.ToListAsync();

        public virtual async Task<TEntity?> GetAsync(int id)
            => await _dbSet.FirstOrDefaultAsync(e => e.Id == id);

        public void Update(TEntity entity)
            => _dbSet.Update(entity);
    }
}