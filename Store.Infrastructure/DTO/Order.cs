using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Infrastructure.DTO
{
    public class Order
    {
        public int OrderId { get; set; }

        public int CustomerId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public double TotalAmount { get; set; }

        public DateTime OrderDate { get; set; }

        public string Status { get; set; } = null!;

  
    }
}
  

