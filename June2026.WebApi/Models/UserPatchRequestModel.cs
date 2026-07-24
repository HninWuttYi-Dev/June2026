using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace June2026.WebApi.Models
{
    public class UserPatchRequestModel
    {
        public string? Username {get; set;}
        public string? Password {get; set;}
    }
    public class UserPatchResponseModel
    {
        public Boolean isSuccess {get; set;}
        public string Message {get; set;}
    }
}