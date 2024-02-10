namespace PetStore.Infrastructure.Repositories
{
    public class Repository<T>(ApplicationDbContext context) : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<IEnumerable<T>> GetAll() => await _context.Set<T>().AsNoTracking().ToListAsync();

        public async Task<T> GetByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Id Not Found");
            var getById = await _context.Set<T>().FindAsync(id);

            return getById ?? throw new ArgumentException($"Entity with Id {id} not found");
        }

        public async Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _context.Set<T>();
            foreach (var item in includes)
                query = query.Include(item);

            var result = await query.FirstOrDefaultAsync();

            return result!;
        }

        public async Task<T> Add(T entity)
        {
            if (entity is null)
                throw new ArgumentException("Entity is null");

            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<T> Update(int id, T entity)
        {
            if (id <= 0)
                throw new ArgumentException("Id Not Found");

            T existingEntity = await GetByIdAsync(id) ?? throw new ArgumentException($"Entity with Id {id} not found");

            _context.Set<T>().Update(existingEntity);
            await _context.SaveChangesAsync();

            return existingEntity;
        }

        public async Task Delete(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Id Not Found");

            T entityToDelete = await GetByIdAsync(id) ?? throw new ArgumentException($"Entity with Id {id} not found");

            _context.Set<T>().Remove(entityToDelete);
            await _context.SaveChangesAsync();
        }

        public Task<int> Count() => _context.Set<T>().CountAsync();
    }
}
