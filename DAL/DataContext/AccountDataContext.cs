using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PBL6.BookStore.Models.Entities.Account;

namespace DAL.DataContext
{
    public class AccountDataContext : IdentityDbContext
    {
        public AccountDataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartDetail> CartDetails { get; set; }
    }
}