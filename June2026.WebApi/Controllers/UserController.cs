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
        public async Task<IActionResult> GetUsersAsync()
        {
            var result =await _userService.GetUsersAsync(new UserListRequestModel());
            if (!result.isSuccess) return BadRequest(result);
            return Ok(result);
        }
        //api/user/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserAsync(int id)
        {
            var result = await _userService.GetUserAsync(new UserEditRequestModel { UserId = id });
            if (!result.isSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> CreateUserAsync([FromBody] UserCreateRequestModel requestModel)
        {
            var result =await _userService.CreateUserAsync(requestModel);
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
        public async Task<IActionResult> UpdateUserAsync(int id, UserPatchRequestModel requestModel)
        {
            requestModel.UserId = id;
            var result = await _userService.UpdateUserAsync(requestModel);
            if (!result.isSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        //api/user?UserId  [FromQuery]
        [HttpDelete("{UserId}")]
        public async Task<IActionResult> DeleteUserAsync([FromRoute] UserDeleteRequestModel requestModel)
        {
            var result = await _userService.DeleteUserAsync(requestModel);
            if (!result.isSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

    }


}