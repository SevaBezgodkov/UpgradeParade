using Server.Data.Repositories.Interfaces;
using Shared.Models;

namespace Server.Data.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _context;
        public CustomerRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateCustomer(Customer customer)
        {
            try { 
                var newCustomer = new Customer
                {
                    Id = customer.Id,
                    FullName = customer.FullName,
                    Email = customer.Email,
                    Phone = customer.Phone
                };

                await _context.AddAsync(newCustomer);
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
