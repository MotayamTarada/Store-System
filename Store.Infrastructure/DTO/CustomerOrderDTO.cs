using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Infrastructure.DTO
{
    public class CustomerOrderDTO
    {

        public string Name { get; set; }
        public string Telephone { get; set; }
        public string Address { get; set; } 
        public List<OrderProductDTO> Products { get; set; }
    }


}

