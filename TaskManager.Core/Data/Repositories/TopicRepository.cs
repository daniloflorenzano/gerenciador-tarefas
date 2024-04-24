using TaskManager.Core.Models;
using TaskManager.Core.Interfaces;

namespace TaskManager.Core.Data.Repositories;

public class TopicRepository : RepositoryBase<Topic, TaskManagerContext>
{
    public TopicRepository(TaskManagerContext context) : base(context)
    {
    }
}
