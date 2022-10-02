using Microsoft.EntityFrameworkCore;
using PBL6.BookStore.Models.Entities.Order;

namespace DAL.DataContext
{
    public class OrderDataContext : DbContext
    {
        public OrderDataContext(DbContextOptions options) : base(options)
        {
        }

        private DbSet<Order> Orders { get; set; }
        private DbSet<OrderDetail> OrderDetails { get; set; }
        private DbSet<Payment> Payments { get; set; }
    }
}