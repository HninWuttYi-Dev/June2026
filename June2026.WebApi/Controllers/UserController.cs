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
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        //api/user
        [HttpGet]
        public IActionResult GetUsers()
        {
            var result = _userService.GetUsers(new UserListRequestModel());
            if (!result.isSuccess) return BadRequest(result);
            return Ok(result);
        }
        //api/user/1
        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            var result = _userService.GetUser(new UserEditRequestModel { UserId = id });
            if (!result.isSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpPost]
        public IActionResult CreateUser([FromBody] UserCreateRequestModel requestModel)
        {
            var result = _userService.CreateUser(requestModel);
            if (!result.isSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
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
            var result = _userService.UpdateUser(requestModel);
            if (!result.isSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        //api/user?UserId  [FromQuery]
        [HttpDelete("{UserId}")]
        public IActionResult DeleteUser([FromRoute] UserDeleteRequestModel requestModel)
        {
            var result = _userService.DeleteUser(requestModel);
            if (!result.isSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

    }


}