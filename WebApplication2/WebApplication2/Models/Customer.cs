using CustomerApp.Models;

namespace WebApplication2.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public int CustomerType { get; set; }
        public List<Transaction> Transactions { get; set; }
    }
}
