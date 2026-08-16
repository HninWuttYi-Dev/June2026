

namespace June2026.Domain.Models
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