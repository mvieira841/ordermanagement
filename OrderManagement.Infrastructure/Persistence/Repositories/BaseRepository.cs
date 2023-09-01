using System.Linq.Expressions;

namespace OrderManagement.Infrastructure.Persistence.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T : Entity
{
    protected readonly ApplicationDbContext _context;

    public BaseRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public virtual async Task<T> GetById(int id) =>
        await _context.Set<T>().FindAsync(id);

    public virtual async Task<T> GetOneByCondition(Expression<Func<T, bool>> expression) =>
        await Task.Run(() => _context.Set<T>().FirstOrDefault(expression));

    public virtual async Task<IEnumerable<T>> GetAll() =>
        await _context.Set<T>().ToListAsync();

    public virtual async Task<IQueryable<T>> GetAll(bool trackChanges) =>
        !trackChanges ? await Task.Run(() => _context.Set<T>().AsNoTracking()) : await Task.Run(() => _context.Set<T>());

    public virtual async Task<IQueryable<T>> GetByCondition(Expression<Func<T, bool>> expression, bool trackChanges) =>
        !trackChanges ? await Task.Run(() => _context.Set<T>().Where(expression).AsNoTracking()) : await Task.Run(() => _context.Set<T>().Where(expression));

    public async Task<IQueryable<T>> GetQueryable(bool trackChanges = false, IEnumerable<Sort>? sort = null) =>
        !trackChanges ? await Task.Run(() => _context.Set<T>().AsNoTracking().Sort(sort)) : await Task.Run(() => _context.Set<T>().Sort(sort));

    public virtual async Task Add(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
    }

    public virtual void Delete(T entity) =>
        _context.Set<T>().Remove(entity);

    public virtual void Update(T entity) =>
        _context.Set<T>().Update(entity);
}