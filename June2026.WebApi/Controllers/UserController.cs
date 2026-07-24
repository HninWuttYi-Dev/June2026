using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using June2026.Database.AppDbContextModels;
using June2026.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace June2026.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _db;
        public UserController()
        {
            _db = new AppDbContext();
        }
        //api/user
        [HttpGet]
        public IActionResult GetUsers()
        {
            var lst = _db.TblUsers.ToList();
            return Ok(lst);
        }
        //api/user/1
        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            var item = _db.TblUsers.FirstOrDefault(x => x.UserId == id);
            if (item is null)
            {
                return NotFound("User not found");
            }
            return Ok(item);

        }
        [HttpPost]
        public IActionResult CreateUser([FromBody] UserCreateRequestModel requestModel)
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
                isSuccess = result > 0,
                Message = result > 0 ? "Create new user successful" : "Failed to create",
                UserId = user.UserId
            };
            return Ok(model);
        }
        [HttpPut]
        public IActionResult UpsertUser()
        {
            return Ok("Upsert user");
        }
        [HttpPatch("{id}")]
        public IActionResult UpdateUser(int id, UserPatchRequestModel requestModel)
        {
            var item = _db.TblUsers.FirstOrDefault(x => x.UserId == id);
            if (item is null)
            {
                return NotFound(new UserPatchResponseModel
                {
                    Message = "User doesn't exist"
                });
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
                isSuccess = result > 0,
                Message = result > 0 ? "Update user successful" : "failed to update user"
            };
            return Ok(model);
        }
        //api/user?UserId  [FromQuery]
        [HttpDelete("{UserId}")]
        public IActionResult DeleteUser([FromRoute] UserDeleteRequestModel requestModel)
        {
            var item = _db.TblUsers.FirstOrDefault(x => x.UserId == requestModel.UserId);
            if (item is null)
            {
                return NotFound(new UserDeleteResponseModel
                {
                    Message = "User is not found"
                });
            }
            _db.Remove(item);
            int result = _db.SaveChanges();
            UserDeleteResponseModel model = new UserDeleteResponseModel
            {
                isSuccess = result > 0,
                Message = result > 0 ? "Delete Successful" : "Failed to delete"
            };
            return Ok(model);

        }

    }


}