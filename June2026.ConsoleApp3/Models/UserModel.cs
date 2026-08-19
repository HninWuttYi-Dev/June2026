using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace June2026.ConsoleApp3.Models
{
    public class GetAllUserModel
    {
        public int UserId { get; set; }

        public string Username { get; set; } = null!;

        public string Password { get; set; } = null!;

    }
    public class UserListResponseModel
    {
        public bool isSuccess { get; set; }
        public string Message { get; set; }
        public List<GetAllUserModel> Users { get; set; }
    }
    public class UserCreateRequestModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
    public class UserCreateResponseModel
    {
        public Boolean isSuccess { get; set; }
        public string Message { get; set; }
        public int UserId { get; set; }
    }
    public class UserPatchRequestModel
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
    }
    public class UserPatchResponseModel
    {
        public Boolean isSuccess { get; set; }
        public string Message { get; set; }
    }
    public class UserDeleteRequestModel
    {
        public int UserId { get; set; }
    }
    public class UserDeleteResponseModel
    {
        public Boolean isSuccess { get; set; }
        public string Message { get; set; }
    }
}