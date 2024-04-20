using Microsoft.EntityFrameworkCore;

namespace TaskManager.Models.Data;

public abstract class RepositoryBase<TEntity, TContext> : IDisposable, IRepositoryBase<TEntity, TContext>
    where TEntity : class where TContext : DbContext
{
    private readonly DbContext _context;
    private readonly DbSet<TEntity> _dbSet;
    

    public RepositoryBase()
    {
        _context = (TContext)Activator.CreateInstance(typeof(TContext));
        _dbSet = _context.Set<TEntity>();
    }

    public void Dispose() => _context.Dispose();

    public async Task UpdateAsync(TEntity entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(TEntity entity)
    {
        _context.Entry(entity).State = EntityState.Deleted;
        await _context.SaveChangesAsync();
    }

    public async Task CreateAsync(TEntity entity)
    {
        _dbSet.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<TEntity?> GetByKeyAsync(params object[] keyValues)
    {
        return await _dbSet.FindAsync(keyValues);
    }

    public async Task<TEntity?> GetByKeyAsNoTrackingAsync(params object[] keyValues)
    {
        var obj = await _dbSet.FindAsync(keyValues);

        if (obj != null)
        {
            _context.Entry(obj).State = EntityState.Detached;
        }

        return obj;
    }

    public async Task<List<TEntity>> ListAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<List<TEntity>> ListAllAsNoTrackingAsync()
    {
        return await _dbSet.AsNoTracking().ToListAsync();
    }
}