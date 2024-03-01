using Shared.Models;

namespace Server.Data.Repositories.Interfaces
{
    public interface ICustomerRepository
    {
        public Task CreateCustomer(Customer customer);
    }
}
