using TaskManagementAPI.Models;

namespace TaskManagementAPI.Repositories
{
    public class TaskCommentRepository : GenericRepository<TaskComment>
    {
        public TaskCommentRepository(TaskManagementDbContext context) : base(context)
        {
        }
    }
}
