using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace June2026.Domain.Models
{
    public class UserEditRequestModel
    {
        public int UserId {get;set;}
    }
    public class UserEditResponseModel
    {
        public bool isSuccess { get; set; }
        public string Message { get; set; }
        public int UserId {get; set;}
        public string Username {get;set;}
    }
}