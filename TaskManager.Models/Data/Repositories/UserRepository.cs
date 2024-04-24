using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Models.Models;

namespace TaskManager.Models.Data.Repositories
{
    public class UserRepository : RepositoryBase<User, TaskManagerContext>
    {
        public UserRepository(TaskManagerContext context) : base(context)
        {
        }
    }
}
