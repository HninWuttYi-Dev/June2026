using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using June2026.Database.AppDbContextModels;
using June2026.Domain.Models;

namespace June2026.Domain.Features.UserFeatures
{
    public class UserService
    {
        private readonly AppDbContext _db;
        public UserService()
        {
            _db = new AppDbContext();
        }

        public UserListResponseModel GetUsers(UserListRequestModel requestModel)
        {
            try
            {
                var lst = _db.TblUsers.ToList();
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

        public UserEditResponseModel GetUser(UserEditRequestModel requestModel)
        {
            try
            {
                var item = _db.TblUsers.FirstOrDefault(x => x.UserId == requestModel.UserId);
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

        public UserCreateResponseModel CreateUser(UserCreateRequestModel requestModel)
        {
            try
            {
                TblUser user = new TblUser
                {
                    Username = requestModel.Username,
                    Password = requestModel.Password
                };
                _db.TblUsers.Add(user);
                int result = _db.SaveChanges(); //0 or 1
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


        public UserPatchResponseModel UpdateUser(UserPatchRequestModel requestModel)
        {
            try
            {
                var item = _db.TblUsers.FirstOrDefault(x => x.UserId == requestModel.UserId);
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

        public UserDeleteResponseModel DeleteUser(UserDeleteRequestModel requestModel)
        {
            try
            {
                var item = _db.TblUsers.FirstOrDefault(x => x.UserId == requestModel.UserId);
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

