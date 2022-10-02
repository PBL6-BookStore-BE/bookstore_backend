using DAL.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Builder.Order
{
    internal class Program
    {
        private static DbContext context = null;
        private static string db;

        private static void Main(string[] args)
        {
            try
            {
                string env = "Local";
                if (args.Length > 0)
                {
                    env = args[0];
                }
                if (env == "Prod")
                {
                    db = args[1];
                }
                else
                {
                    // Add package Microsoft.Extensions.Configuration.Json for AddJsonFile
                    var config = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build();
                    db = config[$"OrderDataContext:{env}"];
                }
                Console.Title = $"Order Builder -{env}";
                Console.WriteLine(db);
                var builder = new DbContextOptionsBuilder<OrderDataContext>();
                //Add package Microsoft.EntityFrameworkCore.SqlServer for UseSqlServer
                builder.UseSqlServer("Server=localhost;Database=pbl6.order;Trusted_Connection=true;MultipleActiveResultSets=true;");
                context = new OrderDataContext(builder.Options);
                RecreateDatabase();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);

                throw;
            }
        }

        internal static void CreateDB()
        {
            Console.WriteLine("Create Database");
            context.Database.EnsureCreated();
        }

        internal static void DeleteDB()
        {
            Console.WriteLine("Delete Database");
            context.Database.EnsureDeleted();
        }

        internal static void RecreateDatabase()
        {
            DeleteDB();
            CreateDB();
        }
    }
}