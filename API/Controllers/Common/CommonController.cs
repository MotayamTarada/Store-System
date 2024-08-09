using Microsoft.AspNetCore.Mvc;
using Store.Repositories.IRepository;

namespace Store.API.Controllers.Common
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommonController : Controller
    {
        private readonly OrderIRepository _orderIRepository;
        private readonly ProductIRepository _productIRepository;
        private readonly CustomerIRepository _customerIRepository;
        private readonly ModuleIRepository _moduleIRepository;  
        private readonly RoleIRepository _roleIRepository;
        private readonly RoleModuleIRepository _roleModuleIRepository;
        private RoleuserIRepository _roleuserIRepository;

        public CommonController(OrderIRepository orderIRepository , ProductIRepository productIRepository
            , CustomerIRepository customerIRepository, ModuleIRepository moduleIRepository, RoleIRepository roleIRepository, RoleModuleIRepository roleModuleIRepository, RoleuserIRepository roleuserIRepository)
        {
            _customerIRepository = customerIRepository;
            _orderIRepository = orderIRepository;
            _productIRepository = productIRepository;
            _moduleIRepository = moduleIRepository;
            _roleIRepository = roleIRepository;
            _roleModuleIRepository = roleModuleIRepository;
            _roleuserIRepository = roleuserIRepository;
        }
        [HttpGet("GetAllPremissionByUserId")]
        public ActionResult GetAllPremissionByUserId(int userId) {
        
            List<int> lstRoles = _roleuserIRepository.Find(Obj => Obj.CustomerId == userId).Select(obj => obj.RoleId).ToList();
            List<int> lstModule = _roleModuleIRepository.Find(obj => lstRoles.Contains(obj.RoleId)).Select(obj => obj.ModuleId).Distinct().ToList();
           Store.Infrastructure.DTO.MenuPermission menuPermission =new Infrastructure.DTO.MenuPermission();
            
            if(lstModule.Contains(1)) 
                menuPermission.Order = "True";
            if (lstModule.Contains(2))
                menuPermission.Product = "True";
            if (lstModule.Contains(3))
                menuPermission.Customer = "True";


            return Ok(menuPermission);
        }


        [HttpGet("Index")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
