using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using June2026.Database.AppDbContextModels;
using June2026.Domain.Features.UserFeatures;
using June2026.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace June2026.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        public UserController()
        {
            _userService = new UserService();
        }
        //api/user
        [HttpGet]
        public IActionResult GetUsers()
        {
           return Ok(_userService.GetUsers(new UserListRequestModel()));
        }
        //api/user/1
        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            return Ok(_userService.GetUser(new UserEditRequestModel{UserId = id}));
        }
        [HttpPost]
        public IActionResult CreateUser([FromBody] UserCreateRequestModel requestModel)
        {
           return Ok(_userService.CreateUser(requestModel));
        }
        // [HttpPut]
        // public IActionResult UpsertUser()
        // {
        //     return Ok("Upsert user");
        // }
        [HttpPatch("{id}")]
        public IActionResult UpdateUser(int id, UserPatchRequestModel requestModel)
        {
            requestModel.UserId = id;
            return Ok(_userService.UpdateUser(requestModel));
        }
        //api/user?UserId  [FromQuery]
        [HttpDelete("{UserId}")]
        public IActionResult DeleteUser([FromRoute] UserDeleteRequestModel requestModel)
        {
            return Ok(_userService.DeleteUser(requestModel));
        }

    }


}