using TaskManager.Models.Interfaces;
using TaskManager.Models.Models;

namespace TaskManager.Models.Data.Repositories;

public class TopicRepository : RepositoryBase<Topic, TaskManagerContext>
{
    public TopicRepository(TaskManagerContext context) : base(context)
    {
    }
}
