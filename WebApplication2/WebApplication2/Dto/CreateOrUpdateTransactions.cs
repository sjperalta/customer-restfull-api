using CustomerApp.Models;

namespace CustomerApp.Dto
{
    public class CreateOrUpdateTransactionsDto
    {
        public List<TransactionDto> Transactions 
        { get; set; } = new List<TransactionDto>();
    }
}
