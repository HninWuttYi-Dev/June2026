using Microsoft.Identity.Client;

namespace June2026.WebApi.Models
{
    public class UserCreateRequestModel
    {
        public string Username {get; set;}
        public string Password {get; set;}
    }
    public class UserCreateResponseModel
    {
        public Boolean isSuccess{get; set;}
        public string Message {get; set;}
        public int UserId {get; set;}
    }
}