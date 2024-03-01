namespace Shared.Models
{
    public class Order
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public DateTime Date { get; set; } = DateTime.Now;

        public string CustomerId { get; set; } = null!;
        public Customer Customer { get; set; } = null!;

        public string ProductId { get; set; } = null!;
        public List<Product> Products { get; set; } = null!;
    }
}
