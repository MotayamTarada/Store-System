   using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Store.Infrastructure.Base;
using System.Runtime.CompilerServices;
using System.Text;

namespace Store.UI.Controllers.Acayear
{
    public class AcayearController : BaseController
    {
        public IActionResult create()
        {
            return View();
        }

        private readonly IConfiguration _configuration;

        public AcayearController(IConfiguration configuration):base(configuration) 
        {
            _configuration = configuration;
        }

        public async Task<IActionResult> AddNewAcayear(Store.Infrastructure.DTO.Acayear newAcayearDTO)
        {
            HttpClient client = new HttpClient();
            String url = "http://localhost:5176/";

            var acayearcontext = JsonConvert.SerializeObject(newAcayearDTO); ;
            var response = await client.PostAsync(url + "api/Acayear/AddNewAcaYear",
                new StringContent(acayearcontext, Encoding.UTF8, "application/json"));

            if(response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                //Good 
            }
            else
            {
                //error
            }
            return View();
        }

    }
}
