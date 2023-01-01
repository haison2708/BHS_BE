using BHS.Domain.SeedWork;

namespace BHS.Infrastructure.Repositories;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class, IAggregateRoot
{
    private readonly BHSDbContext _context;

    public GenericRepository(BHSDbContext context)
    {
        _context = context;
    }

    public IQueryable<TEntity> Get()
    {
        return _context.Set<TEntity>();
    }

    public async Task<TEntity> InsertAsync(TEntity entity)
    {
        var result = await _context.AddAsync(entity);
        return result.Entity;
    }

    public async Task InsertRangeAsync(IEnumerable<TEntity> entities)
    {
        await _context.AddRangeAsync(entities);
    }

    public void Update(TEntity entity)
    {
        _context.Update(entity);
    }

    public void UpdateRange(IEnumerable<TEntity> entities)
    {
        _context.UpdateRange(entities);
    }

    public void Delete(TEntity entity)
    {
        _context.Remove(entity);
    }

    public void DeleteRange(IEnumerable<TEntity> entities)
    {
        _context.RemoveRange(entities);
    }
}