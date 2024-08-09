using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Store.Repositories.IRepository;

namespace Store.API.Controllers.Acayear
{
    [ApiController]
    [Route("api/[controller]")]

    public class AcayearController : Controller
    {
        private readonly AcayearIRepository _acayearIRepository;

        public AcayearController(AcayearIRepository acayearIRepository)
        {
            _acayearIRepository = acayearIRepository;
        }

        [HttpGet("GetAllAcayear")]
        public IActionResult GetAllAcayear()
        {
            var result = _acayearIRepository.GetAll().ToList();
         

            return Ok(result);
        }

        [HttpPost("AddNewAcaYear")]
        public IActionResult AddNewAcaYear(Store.Infrastructure.DTO.Acayear newAcaYear)
        {
            var acyear = new DomainEntities.DBEntities.Acayear
            {
                Id = newAcaYear.Id,
                AcaYearcol = newAcaYear.AcaYearcol
            };
            _acayearIRepository.Add(acyear);
            return Ok("Ok");
        }

        [HttpPost("Update")]
        public IActionResult Update(int id, string name)
        {
            var acyear = _acayearIRepository.GetAll().FirstOrDefault(x => x.Id ==id);
            if (acyear == null)
            {
                return NotFound("Academic year not found");
            }

            acyear.AcaYearcol = name;
            _acayearIRepository.Update(acyear);
            return Ok("Ok");
        }

        [HttpGet("Index")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
