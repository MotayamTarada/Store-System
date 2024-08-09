 using Microsoft.AspNetCore.Mvc;
using Store.Repositories.IRepository;
namespace Store.API.Controllers.Customer
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : Controller
    {

        private readonly CustomerIRepository _customerIRepository;

        public CustomerController(CustomerIRepository customerIRepository)
        {
            _customerIRepository = customerIRepository;
        }
        [HttpPost("AddNewCustomer")]
        public IActionResult AddNewCustomer(Store.Infrastructure.DTO.Customer customerDTO)
        {
            try
            {
                Store.DomainEntities.DBEntities.Customer customer = new Store.DomainEntities.DBEntities.Customer();
                customer.CustomerId = customerDTO.CustomerId;
                customer.Name = customerDTO.Name;
                customer.EMail = customerDTO.EMail;
                customer.Password = customerDTO.Password;
                customer.Telephone = customerDTO.Telephone;
                customer.Address = customerDTO.Address;

                _customerIRepository.Add(customer);

                return Ok(customer);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetCustomerById")]
        public IActionResult GetCustomerById(int id)
        {
            try
            {
                Store.DomainEntities.DBEntities.Customer customer = new Store.DomainEntities.DBEntities.Customer();
                customer = _customerIRepository.GetById(id);
                Infrastructure.DTO.Customer customerDTO = new Infrastructure.DTO.Customer();

                customer.CustomerId = customerDTO.CustomerId;
                customer.Name = customerDTO.Name;
                customer.EMail = customerDTO.EMail;
                customer.Password = customerDTO.Password;
                customer.Telephone = customerDTO.Telephone;
                customer.Address = customerDTO.Address;
                return Ok(customer);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetCustomer")]
        public IActionResult GetCustomer(int id)
        {
            try
            {
                // Find customer by ID and filter by other fields
                var customer = _customerIRepository.Find(c => c.CustomerId == id && !string.IsNullOrEmpty(c.Name) && !string.IsNullOrEmpty(c.EMail) && !string.IsNullOrEmpty(c.Telephone)
                && !string.IsNullOrEmpty(c.Address)).FirstOrDefault();

                if (customer == null)
                {
                    return NotFound("Customer not found");
                }

                return Ok(customer);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("GetAllCustomer")]

        public IActionResult GetAllCustomer()
        {
            try
            {
                List<Store.Infrastructure.DTO.Customer> list = (from obj in _customerIRepository.GetAll()
                                                                select new Store.Infrastructure.DTO.Customer
                                                                {
                                                                    CustomerId = obj.CustomerId,
                                                                    Name = obj.Name,
                                                                    EMail = obj.EMail,
                                                                    Telephone = obj.Telephone,
                                                                    Password = obj.Password,
                                                                    Address = obj.Address

                                                                }).ToList();
                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("Index")]
        public IActionResult Index()
        {
            return View();
        }


    }
}
