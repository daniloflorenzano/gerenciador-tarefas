using Microsoft.EntityFrameworkCore;
using TaskManager.Core.Models;

namespace TaskManager.Core.Data.Repositories;

public class TaskRepository : RepositoryBase<MTask, TaskManagerContext>
{
    public TaskRepository(TaskManagerContext context) : base(context)
    {
    }

    public override async Task<List<MTask>> ListAllAsync()
    {
        return await _context.Tasks
            .Include(x => x.Topic)
            .Include(x => x.User)
            .ToListAsync();
    }
    
    public override async Task<MTask?> GetByKeyAsync(params object[] keyValues)
    {
        return await _context.Tasks
            .Include(x => x.Topic)
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.Id == (int)keyValues[0]);
    }
}