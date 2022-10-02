using Microsoft.EntityFrameworkCore;
using PBL6.BookStore.Models.Entities.Book;

namespace DAL.DataContext
{
    public class BookDataContext : DbContext
    {
        public BookDataContext(DbContextOptions options) : base(options)
        {
        }

        private DbSet<Book> Books { get; set; }
        private DbSet<Author> Authors { get; set; }
        private DbSet<BookAuthor> BookAuthors { get; set; }
        private DbSet<Category> Categories { get; set; }
        private DbSet<Publisher> Publishers { get; set; }
        private DbSet<Review> Reviews { get; set; }
    }
}