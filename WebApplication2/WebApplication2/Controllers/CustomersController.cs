using CustomerApp.Dto;
using CustomerApp.Models;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    //localhost:4545/api/customers
    [ApiController]
    [Route("api/[Controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly CustomerDbContext _context;
        public CustomersController(CustomerDbContext context)
        {
            _context = context;
        }

        //[GET] localhost:4545/api/customers
        [HttpGet]
        public ActionResult<List<Customer>> GetCustomers()
        {
            //select * from db.customers
            var customers = _context.Customers.ToList();
            return Ok(customers);
        }

        [HttpGet("{id}")]
        public ActionResult<Customer> GetCustomer(int id)
        {
            //select top 1 a.* from customer a where customerId = id
            var customer = _context.Customers.Find(id);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        ////api/customers/transactions(2)
        [HttpGet("transactions{id}")]
        //[Route("[controller]/[action]/trasactions")]
        public ActionResult<List<Transaction>> GetCustomerTransactions(int id)
        {
            //select top 1 a.* from customer a where customerId = id
            var transactions = _context.Transactions
                .Where(a => a.CustomerId == id)
                .ToList();

            var customer = _context.Customers.Find(id);
            
            if (!transactions.Any())
            {
                return NotFound();
            }

            return Ok(customer?.Transactions.Select(a => new Transaction { CustomerId = a.CustomerId, Amount = a.Amount, Description = a.Description }).ToList());
        }

        //[POST] localhost:4545/api/customers
        [HttpPost]
        public ActionResult<Customer> CreateCustomer(CustomerCreateDto customer)
        {
            try
            {
                //insert(id, name) values(1, 'Sergio')
                var customerMapping = new Customer { 
                    CustomerName = customer.CustomerName, 
                    CustomerType = customer.CustomerType 
                };
                _context.Customers.Add(customerMapping);
                //commit
                _context.SaveChanges();
                return Ok(customer);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult UpdateCustomer(int id, Customer customer) 
        { 
            //update customer set id = 14, name = 'Sergio Peralta'
            if(id != customer.CustomerId)
            {
                return BadRequest();
            }

            _context.Customers.Update(customer);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteCustomer(int id)
        {
            var customer = _context.Customers.Find(id);

            if (customer == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customer);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
