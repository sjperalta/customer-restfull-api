namespace WebApplication2.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CustomerType { get; set; }
    }
}
