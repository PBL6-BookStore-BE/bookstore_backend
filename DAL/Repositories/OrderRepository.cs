using DAL.DataContext;
using DAL.Interfaces;

namespace DAL.Repositories
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