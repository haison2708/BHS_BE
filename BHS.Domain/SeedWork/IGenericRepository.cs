namespace BHS.Domain.SeedWork;

public interface IGenericRepository<TEntity> where TEntity : class, IAggregateRoot
{
    IQueryable<TEntity> Get();
    Task<TEntity> InsertAsync(TEntity entity);
    Task InsertRangeAsync(IEnumerable<TEntity> entities);
    void Update(TEntity entity);
    void UpdateRange(IEnumerable<TEntity> entities);
    void Delete(TEntity entity);
    void DeleteRange(IEnumerable<TEntity> entities);
}