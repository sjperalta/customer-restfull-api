using WebApplication2.Models;

namespace CustomerApp.Models
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public int CustomerId { get; set; }
        public string? Description { get; set; }
        public decimal Amount { get; set; }
        public int Type { get; set; }
        public Customer? Customer { get; set; }
    }
}
