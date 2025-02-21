using Microsoft.AspNetCore.Mvc;
using TaskManagementAPI.Models;

namespace TaskManagementAPI.Repositories
{
    public class UserRepository : GenericRepository<User>
    {
        public UserRepository(TaskManagementDbContext context) : base(context)
        {
        }

    }
}
