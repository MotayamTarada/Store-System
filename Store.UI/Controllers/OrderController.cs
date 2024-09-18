using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Store.Infrastructure.Base;
using Store.Infrastructure.DTO;
using System.Security.Cryptography;
using System.Text;

namespace Store.UI.Controllers
{
    public class OrderController : BaseController
    {

        private readonly IConfiguration _configuration;

        public OrderController(IConfiguration configuration) : base(configuration)
        {

            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        //Use Latter the method 
        public async Task<IActionResult> AddNewOrder(Store.Infrastructure.DTO.Order orderDTO)
        {
            Infrastructure.Base.BaseController checkLogin = new Infrastructure.Base.BaseController(_configuration);


            String url = _configuration.GetSection("APIURL").Value.ToString();
            HttpClient client = new HttpClient();
            var orderDTOContext = JsonConvert.SerializeObject(orderDTO);
            var response = await client.PostAsync(url + "api/Order/AddNewOrder", new StringContent(orderDTOContext, Encoding.UTF8, "application/json"));

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {

                return View("GetAllOrder");

            }
            else
            {
                return View("Error");
            }
        }

        public async Task<IActionResult> placeOrder(int pid, int cid)
        {
            try
            {
                var orderDTO = new
                {
                    ProductId = pid,
                    CustomerId = cid,
                    Quantity = 1, // Default quantity
                    OrderDate = DateTime.Now,
                    Status = "Waiting"
                };

                string url = _configuration.GetSection("APIURL").Value;
                using HttpClient client = new HttpClient();
                var orderDTOContext = JsonConvert.SerializeObject(orderDTO);
                var response = await client.PostAsync(url + "api/Order/PlaceOrder",
                    new StringContent(orderDTOContext, Encoding.UTF8, "application/json")
                );

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetOrder", new { id = cid });

                }
                else
                {
                    return View();
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Error {ex.Message}");
            }
        }


        public async Task<IActionResult> GetByIdOrder(int id)
        {
            string url = _configuration.GetSection("APIURL").Value.ToString();
            try
            {
                HttpClient client = new HttpClient();
                var response = await client.GetAsync(url + "api/Order/GetOrderById?id=" + id);
                var apiResponse = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<Store.Infrastructure.DTO.Order>>(apiResponse);
                return View(result);

            }
            catch (Exception ex)
            {

                return View("Error");
            }
        }

        public async Task<IActionResult> GetOrder(int id)
        {
            string url = _configuration.GetSection("APIURL").Value.ToString();
            try
            {
                HttpClient client = new HttpClient();
                var response = await client.GetAsync(url + "api/Order/GetOrder?id=" + id);
                var apiResponse = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<Store.Infrastructure.DTO.Order>>(apiResponse);
                return View(result);
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }




        public async Task<IActionResult> GetAllOrder()
        {
            string url = _configuration.GetSection("APIURL").Value.ToString();
            try
            {
                HttpClient client = new HttpClient();
                var response = await client.GetAsync(url + "api/Order/GetAllOrder");
                var apiResponse = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<Store.Infrastructure.DTO.Order>>(apiResponse);
                return View(result);

            }
            catch (Exception ex)
            {

                return View("Error");
            }

        }
        //Update the GetOrders Report Form Admin
        public async Task<IActionResult> GetAllOrders(DateTime? startDate, DateTime? endDate)
        {
            string url = _configuration.GetSection("APIURL").Value.ToString();
            try
            {
                using HttpClient client = new HttpClient();
                var response = await client.GetAsync(url + $"api/Order/GetAllOrders?startDate={startDate:yyyy-MM-dd}&endDate={endDate:yyyy-MM-dd}");
                var apiResponse = await response.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<List<OrderProductDTO>>(apiResponse);

                return View(result);
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }


        //public async Task<IActionResult> GetAllOrders()
        //{
        //    string url = _configuration.GetSection("APIURL").Value.ToString();
        //    try
        //    {
        //        HttpClient client = new HttpClient();
        //        var response = await client.GetAsync(url + "api/Order/GetAllOrders");
        //        var apiResponse = await response.Content.ReadAsStringAsync();

        //        // Deserialize directly into a list of OrderProductDTO
        //        var result = JsonConvert.DeserializeObject<List<OrderProductDTO>>(apiResponse);

        //        return View(result); // Pass the list of orders to the view
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log the exception if necessary
        //        return View("Error");
        //    }
        //}







        // Define a class to match the JSON structure

        //GetOrderUser
        public async Task<IActionResult> GetAllOrderUser(int id)
        {
            string url = _configuration.GetSection("APIURL").Value.ToString();
            try
            {
                HttpClient client = new HttpClient();
                var response = await client.GetAsync(url + "api/Order/GetAllOrderUser?id=" + id);
                var apiResponse = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<Store.Infrastructure.DTO.CustomerOrderDTO>>(apiResponse);
                return View(result);

            }
            catch (Exception ex)
            {

                return View("Error");
            }

        }

        public async Task<IActionResult> UpdateUser(Store.Infrastructure.DTO.Order orderDTO)
        {
            string url = _configuration.GetSection("APIURL").Value.ToString();
            try
            {
                HttpClient Client = new HttpClient();

                var ContextDTO = JsonConvert.SerializeObject(orderDTO);
                var response = await Client.PostAsync(url + "api/Order/UpdateOrderUser",
                    new StringContent(ContextDTO, Encoding.UTF8, "application/json"));
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return RedirectToAction("GetOrder", new { id = orderDTO.CustomerId  });

                }
                else
                {
                    return View("~/Views/Home/ErrorPage.cshtml");

                }
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }

        public async Task<IActionResult> UpdateAdmin(int id,Store.Infrastructure.DTO.Order orderDTO)
        {
            string url = _configuration.GetSection("APIURL").Value.ToString();
            try
            {
                HttpClient Client = new HttpClient();

                var ContextDTO = JsonConvert.SerializeObject(orderDTO);
                var response = await Client.PostAsync(url + "api/Order/UpdateOrderAdmin?=", new StringContent(ContextDTO, Encoding.UTF8, "application/json"));
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return RedirectToAction("GetAllOrder");

                }
                else
                {
                    return View("~/Views/Home/ErrorPage.cshtml");

                }
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }

        public async Task<IActionResult> Update(int id)
        {
            string url = _configuration.GetSection("APIURL").Value.ToString();
            HttpClient client = new HttpClient();

            var response = await client.GetAsync(url + "api/Order/GetOrderById?id=" + id);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var apiResponse = await response.Content.ReadAsStringAsync();
                Store.Infrastructure.DTO.Product product = JsonConvert.DeserializeObject<Store.Infrastructure.DTO.Product>(apiResponse);
                return RedirectToAction("GetOrder");

            }
            else
            {
                return View("~/Views/Home/ErrorPage.cshtml");

            }

        }

        public async Task<IActionResult> Delete(int id)
        {
            string url = _configuration.GetSection("APIURL").Value.ToString();
            try
            {
                HttpClient Client = new HttpClient();

                var response = await Client.DeleteAsync(url + "api/Order/DeleteOrder?id=" + id);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return RedirectToAction("GetAllOrder");

                }
                else
                {

                    return View("");

                }

            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }
        //Delet order user

            public async Task<IActionResult> DeleteUser(int id , int cid)
            {
                string url = _configuration.GetSection("APIURL").Value.ToString();
                try
                {
                    HttpClient Client = new HttpClient();

                    var response = await Client.DeleteAsync(url + "api/Order/DeleteOrder?id=" + id);
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        return RedirectToAction("GetOrder",new {id=cid});

                    }
                    else
                    {

                        return View("");

                    }

                }
                catch (Exception ex)
                {
                    return View("Error");
                }


            }

        }
    }


