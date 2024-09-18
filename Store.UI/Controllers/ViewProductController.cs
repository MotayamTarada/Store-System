using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Store.UI.Controllers
{
    public class ViewProductController : Controller
    {

        private readonly IConfiguration _configuration;

        public ViewProductController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult ViewProduct()
        {
            return View();
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
    
    }
}
