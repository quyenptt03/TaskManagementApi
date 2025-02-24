using TaskManagementAPI.Models;

namespace TaskManagementAPI.Repositories
{
    public class TaskLabelRepository : GenericRepository<TaskLabel>
    {
        public TaskLabelRepository(TaskManagementDbContext context) : base(context)
        {
        }
    }
}
