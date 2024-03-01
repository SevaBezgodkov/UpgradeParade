using Shared.Models;

namespace Server.Data.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        public Task AddOrder(Order order);
    }
}
