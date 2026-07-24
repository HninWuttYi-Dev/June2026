using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace June2026.WebApi.Models
{
    public class UserDeleteRequestModel
    {
        public int UserId {get; set;}
    }
    public class UserDeleteResponseModel
    {
        public Boolean isSuccess { get; set; }
        public string Message { get; set; }
    }
}