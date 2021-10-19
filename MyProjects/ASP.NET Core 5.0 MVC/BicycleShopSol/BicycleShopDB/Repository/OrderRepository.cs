using BicycleShopDB.Tables;

namespace BicycleShopDB.Repository
{
    public class OrderRepository
    {
        private readonly BicycleContext _context;

        public OrderRepository(BicycleContext context)
        {
            _context = context;
        }

        public void Insert(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
        }
    }
}
