using Microsoft.EntityFrameworkCore;
using PBL6.BookStore.Models.Entities.Book;

namespace DAL.DataContext
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
    }
}