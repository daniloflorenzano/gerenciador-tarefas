using Microsoft.EntityFrameworkCore;

namespace TaskManager.Models.Data;

public interface IRepositoryBase<TEntity, TContext> where TEntity : class where TContext : DbContext
{
    void Dispose();
    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(TEntity entity);
    Task CreateAsync(TEntity entity);
    Task<TEntity?> GetByKeyAsync(params object[] keyValues);
    Task<TEntity?> GetByKeyAsNoTrackingAsync(params object[] keyValues);
    Task<List<TEntity>> ListAllAsync();
    Task<List<TEntity>> ListAllAsNoTrackingAsync();
}