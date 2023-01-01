using System.Data;
using BHS.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace BHS.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly BHSDbContext _context;

    private readonly Dictionary<Type, object?> _repositories = new();

    public UnitOfWork(BHSDbContext context)
    {
        _context = context;
    }

    public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class, IAggregateRoot
    {
        if (_repositories.ContainsKey(typeof(TEntity)))
            return (IGenericRepository<TEntity>)_repositories[typeof(TEntity)]!;
        IGenericRepository<TEntity> repo = new GenericRepository<TEntity>(_context);
        _repositories.Add(typeof(TEntity), repo);
        return repo;
    }

    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }

    public async Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task CommitTransactionAsync(IDbContextTransaction transaction)
    {
        if (transaction == null) throw new ArgumentNullException(nameof(transaction));
        try
        {
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch
        {
            await RollbackTransaction();
            throw;
        }
        finally
        {
            Dispose();
        }
    }

    public async Task RollbackTransaction()
    {
        try
        {
            await _context.Database.RollbackTransactionAsync();
        }
        finally
        {
            Dispose();
        }
    }

    public IDbContextTransaction BeginTransaction()
    {
        return _context.Database.BeginTransaction(IsolationLevel.ReadCommitted);
    }

    public IExecutionStrategy CreateExecutionStrategy()
    {
        return _context.Database.CreateExecutionStrategy();
    }

    public DbContext DbContext()
    {
        return _context;
    }
}