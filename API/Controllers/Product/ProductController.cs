using Microsoft.AspNetCore.Mvc;
using Store.Repositories.IRepository;
using Store.DomainEntities.DBEntities;
using Store.Infrastructure.DTO;
using System.Xml.Linq;
using System.Drawing;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Razor.Compilation;
using static System.Net.Mime.MediaTypeNames;

namespace Store.API.Controllers.Product
{


    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly ProductIRepository _productIRepository; // read from product 
        private readonly AcayearIRepository _acayearIRepository;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment;
        public ProductController(ProductIRepository productIRepository, AcayearIRepository acayearIRepository, Microsoft.AspNetCore.Hosting.IHostingEnvironment environment) // cunstructer define 
        {
            _productIRepository = productIRepository;
            _acayearIRepository = acayearIRepository;
           _environment = environment;
        }

      
        [HttpPost("AddNewProduct")]
        public ActionResult AddNewProduct([FromForm]Infrastructure.DTO.Product productDTO)
        {
            try
            {
                DomainEntities.DBEntities.Product product = new DomainEntities.DBEntities.Product();

                // Map DTO to entity
                product.ProductId = productDTO.ProductId;
                product.Name = productDTO.Name;
                product.Price = productDTO.Price;
                product.Quantity = productDTO.Quantity;
                product.BriefDescription = productDTO.BriefDescription;
                product.Remark = productDTO.Remark;
                product.Id = productDTO.Id;
                product.Image = productDTO.Image.ToString();
           
                _productIRepository.Add(product);
                return Ok("Success");
            }
            catch (Exception ex)
            {
                return BadRequest($"Fail: {ex.Message}");
            }
        }


        [HttpDelete("DeleteProduct")]
        public ActionResult DeleteProduct(int id)
        {
            try
            {
                DomainEntities.DBEntities.Product product = new DomainEntities.DBEntities.Product();

                product = _productIRepository.GetById(id);
                _productIRepository.Delete(product);

                return Ok("Success");

            }
            catch (Exception ex)
            {
                return BadRequest("Fail");
            }
        }

        [HttpGet("GetProductById")]
        public IActionResult GetProductById(int id)
        {
            DomainEntities.DBEntities.Product prodcut = new DomainEntities.DBEntities.Product();
            prodcut = _productIRepository.GetById(id);

            Infrastructure.DTO.Product productDTO = new Infrastructure.DTO.Product();

            productDTO.ProductId = prodcut.ProductId;
            productDTO.Name = prodcut.Name;
            productDTO.Price = prodcut.Price;
            productDTO.Remark = prodcut.Remark;
            productDTO.Quantity = prodcut.Quantity;
            productDTO.BriefDescription = prodcut.BriefDescription;
            productDTO.Id = prodcut.Id.Value;
            productDTO.Image = prodcut.Image;


            return Ok(productDTO);
        }


        [HttpPost("UpdateProduct")]
        public IActionResult UpdateProduct(Infrastructure.DTO.Product productDTO)
        {
            DomainEntities.DBEntities.Product product = new DomainEntities.DBEntities.Product();
            try
            {
                product = _productIRepository.GetById(productDTO.ProductId);

                product.ProductId = productDTO.ProductId;
                product.Name = productDTO.Name;
                product.Price = productDTO.Price;
                product.Remark = productDTO.Remark;
                product.Quantity = productDTO.Quantity;
                product.BriefDescription = productDTO.BriefDescription;
                product.Id = productDTO.Id;

                _productIRepository.Update(product);
                return Ok("Success");
            }
            catch (Exception ex)
            {
                return BadRequest("Fail");
            }
        }

        [HttpGet("Index")]
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet("GetAllProduct")]
        public IActionResult GetAllProduct()
        {
            List<Infrastructure.DTO.Product> list = (from obj in _productIRepository.GetAll()
                                                     select new Infrastructure.DTO.Product
                                                     {
                                                         ProductId = obj.ProductId,
                                                         Name = obj.Name,
                                                         Price = obj.Price,
                                                         Remark = obj.Remark,
                                                         Quantity = obj.Quantity,
                                                         BriefDescription = obj.BriefDescription,
                                                         Image = obj.Image,
                                                         Id = obj.Id.Value
                                                     }).ToList();
            return Ok(list);
        }

        [HttpGet("GetAllAcaYear")]
        public IActionResult GetAllAcaYear()
        {
            List<Infrastructure.DTO.Acayear> lst = (from obj in _acayearIRepository.GetAll()
                                                    select new Infrastructure.DTO.Acayear
                                                    {
                                                        Id = obj.Id,
                                                        AcaYearcol = obj.AcaYearcol,
                                                    }).ToList();

            
            return Ok(lst);
        }

    }

}
