using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using June2026.Domain.Features.UserFeatures;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Logging;

namespace June2026.MvcAppAJAX.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            // await _userService.GetUsersAsync(new UserListRequestModel());
            return View();
        }
        public async Task<IActionResult> UserList()
        {
            var model = await _userService.GetUsersAsync(new UserListRequestModel());
            return Json(model);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ActionName("Save")]
        public async Task<IActionResult> UserSaveAsync(UserCreateRequestModel requestModel)
        {
            var model = await _userService.CreateUserAsync(requestModel);
            return Json(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            UserEditResponseModel model = await _userService.GetUserAsync(new UserEditRequestModel(){UserId = id});
            return View(model);
        }
        [HttpPost]
        [ActionName("Update")]
        public async Task<IActionResult> UserUpdateAsync(int id, UserPatchRequestModel requestModel)
        {
            requestModel.UserId = id;
            var model = await _userService.UpdateUserAsync(requestModel);
            return Json(model);
        }
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> UserDeleteAsync(UserDeleteRequestModel requestModel) 
        {
            UserDeleteResponseModel model = await _userService.DeleteUserAsync(requestModel);
            return Json(model);
        }
    }
}