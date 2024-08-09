using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Server.HttpSys;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Store.Infrastructure.Base;
using System.Diagnostics;
using System.IO;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Security.Policy;
using System.Text;

namespace Store.UI.Controllers
{
    [RequestFormLimits(MultipartBodyLengthLimit = 1_000_000)]

    public class ProductController : BaseController
    {

      private readonly IConfiguration _configuration;

        public ProductController(IConfiguration configuration): base(configuration) 
        {
            _configuration = configuration;
        }


        public async Task<IActionResult> Create()
        {
            string url = _configuration.GetSection("APIURL").Value.ToString();

            HttpClient client = new HttpClient();
            var response = await client.GetAsync(url+ "api/Product/GetAllAcaYear");
            
            if (response.IsSuccessStatusCode)
            {
                string apiresponse = await response.Content.ReadAsStringAsync();
                var acayears = JsonConvert.DeserializeObject<List<Store.Infrastructure.DTO.Acayear>>(apiresponse);
                ViewBag.Acayear = acayears; 


            }
         
            return View();
        }
        public async Task<IActionResult> AddNewProduct([FromForm] Store.Infrastructure.DTO.Product product)
        {
            try
            {
                // Check if the product fields are populated
                Console.WriteLine($"Product Name: {product.Name}");
                Console.WriteLine($"Product BriefDescription: {product.BriefDescription}");
                Console.WriteLine($"Product Price: {product.Price}");
                Console.WriteLine($"Product Remark: {product.Remark}");
                Console.WriteLine($"Product Quantity: {product.Quantity}");

                string folderName = DateTime.Now.Ticks.ToString();
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files/" + folderName);

                // Create folder if it doesn't exist
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                string fileName = product.IDImage.FileName;
                string fileNameWithPath = Path.Combine(path, fileName);

                using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    await product.IDImage.CopyToAsync(stream);
                }

                product.Image = $"{folderName}/{fileName}";

                using (HttpClient client = new HttpClient())
                {
                  string url = _configuration.GetSection("APIURL").Value.ToString();


                    // Create multipart form data content
                    var formData = new MultipartFormDataContent();
                    formData.Add(new StringContent(product.ProductId.ToString()), "ProductId");
                    formData.Add(new StringContent(product.Name), "Name");
                    formData.Add(new StringContent(product.BriefDescription), "BriefDescription");
                    formData.Add(new StringContent(product.Price.ToString()), "Price");
                    formData.Add(new StringContent(product.Remark), "Remark");
                    formData.Add(new StringContent(product.Quantity.ToString()), "Quantity");
                    formData.Add(new StringContent(product.Image), "Image");
                    formData.Add(new StringContent(product.Id.ToString()), "Id");

                    // Add the file content
                    var fileStreamContent = new StreamContent(new FileStream(fileNameWithPath, FileMode.Open));
                    formData.Add(fileStreamContent, "IDImage", fileName);

                    // Send the request
                    var response = await client.PostAsync(url + "api/Product/AddNewProduct", formData);

                    // Log the response for debugging
                    string responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Response Status Code: {response.StatusCode}");
                    Console.WriteLine($"Response Content: {responseContent}");

                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        return RedirectToAction("GetAllProduct");
                    }
                    else
                    {
                        ViewBag.ErrorMessage = responseContent;
                        return View("Error"); // Pass the response content to an error page view
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception for debugging
                Console.WriteLine($"Exception: {ex.Message}");
                ViewBag.ErrorMessage = ex.Message;
                return View("Error"); // Pass the exception message to the error page view
            }
        }



        public async Task<IActionResult> GetAllProduct()
        {

            string url = _configuration.GetSection("APIURL").Value.ToString();

            try
            {
                HttpClient client = new HttpClient();

                var response = await client.GetAsync(url + "api/Product/GetAllProduct");

                var apiResponse = await response.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<List<Store.Infrastructure.DTO.Product>>(apiResponse);

                return View(result);

            }
            catch (Exception ex)
            {
                return View("~/Views/Home/ErrorPage.cshtml");
            }
        }
        public async Task<IActionResult> GetFiveProduct()
        {

            string url = _configuration.GetSection("APIURL").Value.ToString();

            try
            {
                HttpClient client = new HttpClient();

                var response = await client.GetAsync(url + "api/Product/GetAllProduct");

                var apiResponse = await response.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<List<Store.Infrastructure.DTO.Product>>(apiResponse);

                return View(result);

            }
            catch (Exception ex)
            {
                return View("~/Views/Home/ErrorPage.cshtml");
            }
        }

        public async Task<IActionResult> GetFourProduct()
        {

            string url = _configuration.GetSection("APIURL").Value.ToString();

            try
            {
                HttpClient client = new HttpClient();

                var response = await client.GetAsync(url + "api/Product/GetAllProduct");

                var apiResponse = await response.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<List<Store.Infrastructure.DTO.Product>>(apiResponse);

                return View(result);

            }
            catch (Exception ex)
            {
                return View("~/Views/Home/ErrorPage.cshtml");
            }
        }

        public async Task<IActionResult> GetThreeProduct()
        {

            string url = _configuration.GetSection("APIURL").Value.ToString();
            try
            {
                HttpClient client = new HttpClient();

                var response = await client.GetAsync(url + "api/Product/GetAllProduct");

                var apiResponse = await response.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<List<Store.Infrastructure.DTO.Product>>(apiResponse);

                return View(result);

            }
            catch (Exception ex)
            {
                return View("~/Views/Home/ErrorPage.cshtml");
            }
        }
        public async Task<IActionResult> GetTwoProduct()
        {

            string url = _configuration.GetSection("APIURL").Value.ToString();
            try
            {
                HttpClient client = new HttpClient();

                var response = await client.GetAsync(url + "api/Product/GetAllProduct");

                var apiResponse = await response.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<List<Store.Infrastructure.DTO.Product>>(apiResponse);

                return View(result);

            }
            catch (Exception ex)
            {
                return View("~/Views/Home/ErrorPage.cshtml");
            }
        }

        public async Task<IActionResult> GetOneProduct()
        {

            string url = _configuration.GetSection("APIURL").Value.ToString();
            try
            {
                HttpClient client = new HttpClient();

                var response = await client.GetAsync(url + "api/Product/GetAllProduct");

                var apiResponse = await response.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<List<Store.Infrastructure.DTO.Product>>(apiResponse);

                return View(result);

            }
            catch (Exception ex)
            {
                return View("~/Views/Home/ErrorPage.cshtml");
            }
        }

     


        public async Task<IActionResult> GetProduct(int id)
        {
            string url = _configuration.GetSection("APIURL").Value;
            if (string.IsNullOrEmpty(url))
            {
                return View("~/Views/Home/ErrorPage.cshtml"); // API URL not configured
            }

            try
            {
                HttpClient client = new HttpClient();
                var response = await client.GetAsync($"{url}api/Product/GetProductById?id={id}");

                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<Store.Infrastructure.DTO.Product>(apiResponse);

                    return View(result);
                }
                else
                {
                    return View("~/Views/Home/ErrorPage.cshtml"); // Show error page for bad response
                }
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                // e.g., _logger.LogError(ex, "Error fetching product by id via API");
                return View("Error");
            }
        }







        public async Task<IActionResult> Update (int id )
        {
            string url = _configuration.GetSection("APIURL").Value.ToString();
            HttpClient client = new HttpClient();
            var acaYear = await client.GetAsync(url + "api/Product/GetAllAcaYear");
            string acaYearcontant = await acaYear.Content.ReadAsStringAsync();
            ViewBag.Acayear = JsonConvert.DeserializeObject<List<Store.Infrastructure.DTO.Acayear>>(acaYearcontant);

            var response = await client.GetAsync(url + "api/Product/GetProductById?id=" + id);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var apiResponse = await response.Content.ReadAsStringAsync();
                 Store.Infrastructure.DTO.Product product = JsonConvert.DeserializeObject<Store.Infrastructure.DTO.Product>(apiResponse);
                return View(product);
            }
            else
            {
                return View();
            }
           
        }




        public async Task<IActionResult> UpdateProduct(Infrastructure.DTO.Product product)
        {

            string url = _configuration.GetSection("APIURL").Value.ToString();
            HttpClient client = new HttpClient();

            var ProductContext = JsonConvert.SerializeObject(product);

            var respnse = await client.PostAsync(url + "api/Product/UpdateProduct", new StringContent(ProductContext, Encoding.UTF8, "application/json"));

            if (respnse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return RedirectToAction("GetAllProduct");

            }
            else
            {
                return View("~/Views/Home/ErrorPage.cshtml");
            }
        }


        public async Task<IActionResult> Delete(int id)
        {

            string url = _configuration.GetSection("APIURL").Value.ToString();
            HttpClient clinent = new HttpClient();

            var respnse = await clinent.DeleteAsync(url + "api/Product/DeleteProduct?id="+id);

            if(respnse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return RedirectToAction("GetAllProduct");
            }else {

                return View("~/Views/Home/ErrorPage.cshtml");
                   
            }
        }

        }
}
