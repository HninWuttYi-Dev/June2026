using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace June2026.Domain.Models
{
    public class ProductListRequestModel
    {
        
    }
    public class ProductListResponseModel
    {
        public bool isSuccess {get; set;}
        public string Message {get; set;}
        public List<Product> Products {get; set;}

        public static implicit operator Task<object>(ProductListResponseModel v)
        {
            throw new NotImplementedException();
        }
    }
    public class Product
    {
        public int Id {get; set;}
        public string Name { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }
    }
}