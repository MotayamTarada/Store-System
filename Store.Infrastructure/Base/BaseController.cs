using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Store.Infrastructure.Base
{
    public class BaseController: Controller
    {
        public readonly IConfiguration _configuration; 

          public BaseController(IConfiguration configuration) { 
               this._configuration = configuration;
          }

        public override async void OnActionExecuting(ActionExecutingContext context)
        {
            if(IsUserLoggedIn())
            {
                ViewBag.UserId = HttpContext.User.Claims.Where(obj => obj.Type == "UserID").FirstOrDefault().Value;
            }
            else
            {
                context.Result = RedirectToAction("Login","Customer");
            }
        }
        // check if the user Login Or Not 

        public bool IsUserLoggedIn() { 
          var result = HttpContext.User.Claims.Where(obj=> obj.Type == "UserID").FirstOrDefault();

            if(result == null)
            {
                 return false;
            }
            else
            {
                return true;
            }

        }

        public async  Task<IActionResult> GetAllPremissionByUserId()
        {
            int id = 0;
            if (ViewBag.UserId != null)
            
              id = Convert.ToInt32(ViewBag.UserId);
            
            HttpClient client = new HttpClient();
            String url = _configuration.GetSection("APIURL").Value.ToString();
            var response = await client.GetAsync(url + "api/Common/GetAllPremissionByUserId?userId=" + id);
            Store.Infrastructure.DTO.MenuPermission menuPermission = new Store.Infrastructure.DTO.MenuPermission();

            if(response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var Data = await response.Content.ReadAsStringAsync();
                menuPermission = JsonConvert.DeserializeObject<Store.Infrastructure.DTO.MenuPermission>(Data);

            }
            return PartialView("_ManageMenu",menuPermission);
        }


    }
}
