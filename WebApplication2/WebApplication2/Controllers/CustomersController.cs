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

        //[POST] localhost:4545/api/customers
        [HttpPost]
        public ActionResult<Customer> CreateCustomer(Customer customer)
        {
            try
            {
                //insert(id, name) values(1, 'Sergio')
                _context.Customers.Add(customer);
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
