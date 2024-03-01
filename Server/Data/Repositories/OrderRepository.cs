using Server.Data.Repositories.Interfaces;
using Shared.Models;

namespace Server.Data.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;
        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddOrder(Order order)
        {
            try
            {
                var newOrder = new Order
                {
                    Id = order.Id,
                    Date = DateTime.Now.ToUniversalTime(),
                    CustomerId = order.CustomerId,
                    ProductId = order.ProductId
                };

                await _context.Orders.AddAsync(newOrder);
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
