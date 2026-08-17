using June2026.Domain.Models;

namespace June2026.Domain.Features.UserFeatures
{
    public interface IUserService
    {
        UserCreateResponseModel CreateUser(UserCreateRequestModel requestModel);
        UserDeleteResponseModel DeleteUser(UserDeleteRequestModel requestModel);
        UserEditResponseModel GetUser(UserEditRequestModel requestModel);
        UserListResponseModel GetUsers(UserListRequestModel requestModel);
        UserPatchResponseModel UpdateUser(UserPatchRequestModel requestModel);
    }

}

