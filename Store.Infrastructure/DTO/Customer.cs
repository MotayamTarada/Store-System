using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Infrastructure.DTO
{
    public  class Customer
    {
        public int CustomerId { get; set; }
        
        public string EMail { get; set; } = null!;

        public string Name { get; set; } = null!;


        public string Telephone { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string Address { get; set; } = null!;

    }
}
