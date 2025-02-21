using TaskManagementAPI.Models;

namespace TaskManagementAPI.Repositories
{
    public class CategoryRepository : GenericRepository<Category>
    {
        public CategoryRepository(TaskManagementDbContext context) : base(context)
        {
        }

        //public override void Update(Category entity)
        //{
        //    var customer = context.Customers
        //        .Single(c => c.CustomerId == entity.CustomerId);

        //    customer.Name = entity.Name;
        //    customer.City = entity.City;
        //    customer.PostalCode = entity.PostalCode;
        //    customer.ShippingAddress = entity.ShippingAddress;
        //    customer.Country = entity.Country;

        //}
    }
}
