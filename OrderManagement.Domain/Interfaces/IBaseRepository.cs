namespace OrderManagement.Domain.Interfaces;

public interface IBaseRepository<T> where T : Entity
{
    Task<T> GetById(int id);
    Task<T> GetOneByCondition(Expression<Func<T, bool>> expression);
    Task<IEnumerable<T>> GetAll();
    Task<IQueryable<T>> GetAll(bool trackChanges);
    Task<IQueryable<T>> GetByCondition(Expression<Func<T, bool>> expression, bool trackChanges);
    Task<IQueryable<T>> GetQueryable(bool trackChanges = false, IEnumerable<Sort>? sort = null);
    Task Add(T entity);
    void Delete(T entity);
    void Update(T entity);
}