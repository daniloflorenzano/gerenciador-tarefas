using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Core.Data;
using TaskManager.Core.Models;

namespace TaskManager.Core.Interfaces
{
    public interface IUserRepository : IRepositoryBase<User, TaskManagerContext>
    {
    }
}
