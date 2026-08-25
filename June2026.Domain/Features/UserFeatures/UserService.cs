using June2026.Database.AppDbContextModels;
using June2026.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace June2026.Domain.Features.UserFeatures
{

    public class UserService : IUserService
    {
        private readonly AppDbContext _db;

        public UserService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<UserListResponseModel> GetUsersAsync(UserListRequestModel requestModel)
        {
            try
            {
                var lst = await _db.TblUsers.ToListAsync();
                List<UserModel> users = new List<UserModel>();
                foreach (var item in lst)
                {
                    UserModel user = new UserModel
                    {
                        UserId = item.UserId,
                        Username = item.Username
                    };
                    users.Add(user);
                }

                return new UserListResponseModel
                {
                    // Users = lst.Select(x => new UserModel
                    // {
                    //     UserId = x.UserId,
                    //     Username = x.Username
                    // }).ToList()
                    isSuccess = true,
                    Message = "User fetch successfully",
                    Users = users
                };
            }
            catch (Exception ex)
            {
                return new UserListResponseModel
                {
                    isSuccess = false,
                    Message = ex.ToString()
                };
            }
        }

        public async Task<UserEditResponseModel> GetUserAsync(UserEditRequestModel requestModel)
        {
            try
            {
                var item = await _db.TblUsers.FirstOrDefaultAsync(x => x.UserId == requestModel.UserId);
                if (item is null)
                {
                    return new UserEditResponseModel
                    {
                        isSuccess = false,
                        Message = "User is not found"
                    };
                }
                return new UserEditResponseModel
                {
                    isSuccess = true,
                    Message = "User fetched successfully",
                    UserId = item.UserId,
                    Username = item.Username
                };
            }
            catch (Exception ex)
            {
                return new UserEditResponseModel
                {
                    isSuccess = false,
                    Message = ex.ToString()
                };
            }

        }

        public async Task<UserCreateResponseModel> CreateUserAsync(UserCreateRequestModel requestModel)
        {
            try
            {
                TblUser user = new TblUser
                {
                    Username = requestModel.Username,
                    Password = requestModel.Password
                };
                _db.TblUsers.Add(user);
                int result = await _db.SaveChangesAsync(); //0 or 1
                UserCreateResponseModel model = new UserCreateResponseModel
                {
                    isSuccess = true,
                    Message = "Created new user successfully",
                    UserId = user.UserId
                };
                return model;
            }
            catch (Exception ex)
            {
                return new UserCreateResponseModel
                {
                    isSuccess = false,
                    Message = "Failed to create user"
                };
            }
        }


        public async Task<UserPatchResponseModel> UpdateUserAsync(UserPatchRequestModel requestModel)
        {
            try
            {
                var item = await _db.TblUsers.FirstOrDefaultAsync(x => x.UserId == requestModel.UserId);
                if (item is null)
                {
                    return new UserPatchResponseModel
                    {
                        isSuccess = false,
                        Message = "User doesn't exist"
                    };
                }
                if (!string.IsNullOrEmpty(requestModel.Username))
                {
                    item.Username = requestModel.Username;
                }
                if (!string.IsNullOrEmpty(requestModel.Password))
                {
                    item.Password = requestModel.Password;
                }
                int result = _db.SaveChanges();

                UserPatchResponseModel model = new UserPatchResponseModel
                {
                    isSuccess = true,
                    Message = "Updated user successfully"
                };
                return model;
            }
            catch (Exception ex)
            {

                return new UserPatchResponseModel
                {
                    isSuccess = false,
                    Message = "Failed to update user"
                };
            }
        }

        public async Task<UserDeleteResponseModel> DeleteUserAsync(UserDeleteRequestModel requestModel)
        {
            try
            {
                var item = await _db.TblUsers.FirstOrDefaultAsync(x => x.UserId == requestModel.UserId);
                if (item is null)
                {
                    return new UserDeleteResponseModel
                    {
                        isSuccess = false,
                        Message = "User is not found"
                    };
                }
                _db.Remove(item);
                int result = _db.SaveChanges();
                UserDeleteResponseModel model = new UserDeleteResponseModel
                {
                    isSuccess = true,
                    Message = "User is deleted successfully"
                };
                return model;
            }
            catch (Exception ex)
            {

                return new UserDeleteResponseModel
                {
                    isSuccess = false,
                    Message = "Failed to delete user"

                };
            }

        }

    }

}

