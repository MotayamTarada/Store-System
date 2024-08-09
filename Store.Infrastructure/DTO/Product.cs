using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Infrastructure.DTO
{
    public class Product
    {

        [Key]
        public int ProductId { get; set; }


        public string Name { get; set; } = null!;

        public string BriefDescription { get; set; } = null!;


        public double Price { get; set; }

        public string Remark { get; set; } = null!;

        public int Quantity { get; set; }

        [NotMapped]
        [AllowNull]
        public IFormFile IDImage { get; set; }

        public string Image { get; set; }
        
        public int Id { get; set; }

    }
}

