using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Core.Models;

namespace TaskManager.Core.Data.Repositories
{
    public class UserRepository : RepositoryBase<User, TaskManagerContext>
    {
        public UserRepository(TaskManagerContext context) : base(context)
        {
        }
    }
}
