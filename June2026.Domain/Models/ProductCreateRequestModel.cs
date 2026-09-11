using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace June2026.Domain.Models
{
    public class ProductCreateRequestModel
    {
        public string Name { get; set; } 

        public decimal Price { get; set; }

        public int Quantity { get; set; }
    }
    public class ProductCreateResponseModel
    {
        public bool isSuccess {get; set;}
        public string Message {get; set;}
        public int Id {get; set;}
    }
}