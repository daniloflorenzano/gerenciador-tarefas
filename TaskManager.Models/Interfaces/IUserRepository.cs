using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Models.Models;

namespace TaskManager.Models.Interfaces
{
    public interface IUserRepository : IRepositoryBase<User, TaskManagerContext>
    {
    }
}
