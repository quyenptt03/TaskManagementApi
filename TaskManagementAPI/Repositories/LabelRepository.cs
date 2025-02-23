

using Microsoft.EntityFrameworkCore;
using TaskManagementAPI.Models;

namespace TaskManagementAPI.Repositories
{
    public class LabelRepository : GenericRepository<Label>
    {
        public LabelRepository(TaskManagementDbContext context) : base(context)
        {
        }

    }
}
