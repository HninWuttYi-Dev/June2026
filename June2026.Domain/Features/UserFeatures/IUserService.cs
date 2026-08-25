using June2026.Database.AppDbContextModels;
using June2026.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace June2026.Domain.Features.UserFeatures
{
    public interface IUserService
    {
        Task<UserCreateResponseModel> CreateUserAsync(UserCreateRequestModel requestModel);
        Task<UserDeleteResponseModel> DeleteUserAsync(UserDeleteRequestModel requestModel);
        Task<UserEditResponseModel> GetUserAsync(UserEditRequestModel requestModel);
        Task<UserListResponseModel> GetUsersAsync(UserListRequestModel requestModel);
        Task<UserPatchResponseModel> UpdateUserAsync(UserPatchRequestModel requestModel);
    }

}

