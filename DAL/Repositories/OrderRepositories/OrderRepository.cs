using DAL.DataContext;
using DAL.Interfaces.IOrderRepositories;

namespace DAL.Repositories.OrderRepositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderDataContext _context;

        public OrderRepository(OrderDataContext context)
        {
            _context = context;
        }
    }
}