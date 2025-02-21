using TaskManagementAPI.Models;
using Task = TaskManagementAPI.Models.Task;

namespace TaskManagementAPI.Repositories
{
    public class TaskRepository : GenericRepository<Task>
    {
        public TaskRepository(TaskManagementDbContext context) : base(context)
        {
        }
    }
}
