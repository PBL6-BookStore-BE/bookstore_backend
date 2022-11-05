using MicroserviceBook.Entities;
using Microsoft.EntityFrameworkCore;

namespace MicroserviceBook.Data
{
    public class BookDataContext : DbContext
    {
        public BookDataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<CartDetail>  CartDetails { get; set; }
        public DbSet<Cart> Carts { get; set; }
    }
}