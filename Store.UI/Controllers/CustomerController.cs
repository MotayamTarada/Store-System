using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Newtonsoft.Json;
using Store.Infrastructure.Base;
using System.Security.Claims;
using System.Text;

namespace Store.UI.Controllers
{
    public class CustomerController :Controller
    {
        private readonly IConfiguration _configuration;
        public CustomerController(IConfiguration configuration)
        {

            _configuration = configuration;
        }

        //   Add New Customer --> Registration
        public async Task<IActionResult> AddNewCustomer(Store.Infrastructure.DTO.Customer customerDTO)
        {
            String url = _configuration.GetSection("APIURL").Value.ToString();
            HttpClient client = new HttpClient();
            var customerDTOcontext = JsonConvert.SerializeObject(customerDTO);
            var response = await client.PostAsync(url + "api/Customer/AddNewCustomer",
                new StringContent(customerDTOcontext, Encoding.UTF8, "application/json"));

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return RedirectToAction("Login");

            }
            return View("Error");
        }

        public async Task<IActionResult> GetCustomerById(int id)
        {
            try
            {
                String url = _configuration.GetSection("APIURL").Value;
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.GetAsync(url + "api/Customer/GetCustomerById?id=" + id);
                    if (response.IsSuccessStatusCode)
                    {
                        String apiResponse = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<Store.Infrastructure.DTO.Customer>(apiResponse);

                        return RedirectToAction("GetCustomer", new { id = id });

                    }
                    else
                    {
                        return View("Error");
                    }
                }
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }

        public async Task<IActionResult> GetCustomer(int id)
        {
            try
            {
                String url = _configuration.GetSection("APIURL").Value;
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.GetAsync(url + "api/Customer/GetCustomer?id=" + id);
                    if (response.IsSuccessStatusCode)
                    {
                        String apiResponse = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<Store.Infrastructure.DTO.Customer>(apiResponse);

                        return View(result); // Assuming you want to display the single customer in a view
                    }
                    else
                    {
                        return View("Error");
                    }
                }
            }
            catch (Exception ex)
            {
                // Log exception details if needed
                return View("Error");
            }
        }



        public async Task<IActionResult> GetAllCustomer()
        {
            try
            {
                String url = _configuration.GetSection("APIURL").Value.ToString();

                HttpClient client = new HttpClient();
                var respnse = await client.GetAsync(url + "api/Customer/GetAllCustomer");
                String apiresponse = await respnse.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<Store.Infrastructure.DTO.Customer>>(apiresponse);
                return View(result);
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }

        public IActionResult LoginUser(Store.Infrastructure.DTO.Login loginDTO)
        {
            HttpContext.Response.Cookies.Append("Email", loginDTO.Email);
            var item = LoginUserDetailes(loginDTO).Result;

            if (item != -1)
            {
                var userClaim = new List<Claim>() // Securety 
                { 
                    //claim based authrization
                    new Claim(ClaimTypes.NameIdentifier,loginDTO.Email),
                    new Claim("UserName","Admin@gmail.com"),
                    new Claim("UserID",item.ToString()),
                };
                var userIdentity = new ClaimsIdentity(userClaim, "User Identity");
                var userPrincipal = new ClaimsPrincipal(new[] { userIdentity });
                HttpContext.SignInAsync(userPrincipal);

                return RedirectToAction("Privacy", "Home");

            }
            else
            {
                return View("Login");
            }
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult index()
        {
            return View();
        }
        public async Task<int> LoginUserDetailes(Store.Infrastructure.DTO.Login loginDTO)
        {

            HttpClient client = new HttpClient();
            String url = _configuration.GetSection("APIURL").Value.ToString();
            var LoginContextDTO = JsonConvert.SerializeObject(loginDTO);
            var response = await client.PostAsync(url + "api/Users/Login",
              new StringContent(LoginContextDTO, Encoding.UTF8, "application/json"));

            var Data = await response.Content.ReadAsStringAsync();
            int id = JsonConvert.DeserializeObject<int>(Data);
            return id;
        }

        public async Task<IActionResult> LogOut()
        {

            var _user = HttpContext.User as ClaimsPrincipal;
            var _identity = _user.Identity as ClaimsIdentity;

            foreach (var claim in _user.Claims.ToList())
            {
                _identity.RemoveClaim(claim);
            }

            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}


   




