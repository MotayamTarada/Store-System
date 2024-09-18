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
using Org.BouncyCastle.Utilities;
using Store.DomainEntities;

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
            // Retrieve the product by ID
            var product = _productIRepository.GetById(id);

            if (product == null)
            {
                return NotFound(); // Return a 404 response if the product is not found
            }

            // Map the product entity to a DTO
            var productDTO = new Infrastructure.DTO.Product
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Price = product.Price,
                Remark = product.Remark,
                Quantity = product.Quantity,
                BriefDescription = product.BriefDescription,
                Id = product.Id.HasValue ? product.Id.Value : 0,
                Image = product.Image,
                

            };

            return Ok(productDTO);
        }




        [HttpGet("GetProductMidById")]
        public IActionResult GetProductMidById(int id)
        {
            // Retrieve the product by ID
            var product = _productIRepository.GetById(id);

            if (product == null)
            {
                return NotFound(); // Return a 404 response if the product is not found
            }

            // Map the product entity to a DTO
            var productDTO = new Infrastructure.DTO.ProductMid
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Price = product.Price,
                Remark = product.Remark,
                Quantity = product.Quantity,
                BriefDescription = product.BriefDescription,
             


            };

            return Ok(productDTO);
        }



        [HttpPatch("UpdateProduct")]
        public IActionResult UpdateProduct([FromBody] Infrastructure.DTO.ProductMid productDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var product = _productIRepository.GetById(productDTO.ProductId);
                if (product == null)
                {
                    return NotFound("Product not found");
                }

                // Update the product properties
                product.Name = productDTO.Name;
                product.Price = productDTO.Price;
                product.Remark = productDTO.Remark;
                product.Quantity = productDTO.Quantity;
                product.BriefDescription = productDTO.BriefDescription;
                product.Id = productDTO.Id;
                
                _productIRepository.Update(product);
                return Ok("Product updated successfully");
            }
            catch (Exception ex)
            {
                // Log the exception details
                return BadRequest($"Failed to update product: {ex.Message}");
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
