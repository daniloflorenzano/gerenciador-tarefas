using Microsoft.EntityFrameworkCore;
using TaskManager.Core.Interfaces;

namespace TaskManager.Core.Data;

public abstract class RepositoryBase<TEntity, TContext> : IDisposable, IRepositoryBase<TEntity, TContext>
    where TEntity : class where TContext : DbContext
{
    protected readonly TContext _context;
    protected readonly DbSet<TEntity> _dbSet;
    

    public RepositoryBase(TContext context)
    {
        _context = context;
        _dbSet = _context.Set<TEntity>();
    }

    public void Dispose() => _context.Dispose();

    public virtual async Task UpdateAsync(TEntity entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public virtual async Task DeleteAsync(TEntity entity)
    {
        _context.Entry(entity).State = EntityState.Deleted;
        await _context.SaveChangesAsync();
    }

    public virtual async Task CreateAsync(TEntity entity)
    {
        _dbSet.Add(entity);
        await _context.SaveChangesAsync();
    }

    public virtual async Task<TEntity?> GetByKeyAsync(params object[] keyValues)
    {
        return await _dbSet.FindAsync(keyValues);
    }

    public virtual async Task<TEntity?> GetByKeyAsNoTrackingAsync(params object[] keyValues)
    {
        var obj = await _dbSet.FindAsync(keyValues);

        if (obj != null)
        {
            _context.Entry(obj).State = EntityState.Detached;
        }

        return obj;
    }

    public virtual async Task<List<TEntity>> ListAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public virtual async Task<List<TEntity>> ListAllAsNoTrackingAsync()
    {
        return await _dbSet.AsNoTracking().ToListAsync();
    }
}