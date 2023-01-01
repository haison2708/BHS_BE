using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace BHS.Domain.SeedWork;

public interface IUnitOfWork : IScopedService, IDisposable
{
    Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default);
    IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class, IAggregateRoot;
    Task CommitTransactionAsync(IDbContextTransaction transaction);
    Task RollbackTransaction();
    IDbContextTransaction BeginTransaction();
    IExecutionStrategy CreateExecutionStrategy();

    DbContext DbContext();
}