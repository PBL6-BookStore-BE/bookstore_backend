using Microsoft.EntityFrameworkCore;
using PBL6.BookStore.Models.Entities.Order;

namespace MicroserviceOrder.Data
{
    public class OrderDataContext : DbContext
    {
        public OrderDataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Payment> Payments { get; set; }
    }
}
