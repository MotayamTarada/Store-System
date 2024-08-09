using Microsoft.AspNetCore.Mvc;
using Store.Repositories.IRepository;
using Store.Infrastructure.DTO;
using System.Linq;

namespace Store.API.Controllers.Users
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly CustomerIRepository _customerIRepository;


        public UsersController(CustomerIRepository customerIRepository)
        {
            _customerIRepository = customerIRepository;
        }
        

        [HttpPost("Login")]
        public ActionResult Login(Store.Infrastructure.DTO.Login loginDTO)
        {
            if (loginDTO == null)
            {
                return BadRequest("Invalid login data.");
            }

            var result = _customerIRepository.Find(x => x.EMail == loginDTO.Email && x.Password == loginDTO.Password)
                .FirstOrDefault();

            if (result != null)
            {
                return Ok(result.CustomerId);
            }
            else
            {
                return BadRequest(-1);
            }
        }



    }
}
