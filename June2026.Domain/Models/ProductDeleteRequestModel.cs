using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace June2026.Domain.Models
{
    public class ProductDeleteRequestModel
    {
        public int Id {get; set;}
    }
    public class ProductDeleteResponseModel
    {
        public bool isSuccess { get; set; }
        public string Message { get; set; }
    }
}