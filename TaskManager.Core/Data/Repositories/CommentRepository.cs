using Microsoft.EntityFrameworkCore;
using TaskManager.Core.Models;

namespace TaskManager.Core.Data.Repositories;

public class CommentRepository : RepositoryBase<Comment, TaskManagerContext>
{
    public CommentRepository(TaskManagerContext context) : base(context)
    {
    }

    public async Task<List<Comment>> GetAllCommentsFromTask(int taskId)
    {
        using (_context)
        {
            return await _context
                .Comments.Where(c => c.TaskId == taskId).ToListAsync();
        }
    }
}