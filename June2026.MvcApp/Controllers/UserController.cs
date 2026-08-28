using June2026.Domain.Features.UserFeatures;
using Microsoft.AspNetCore.Mvc;

namespace June2026.MvcApp.Controllers;

public class UserController : Controller
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    // GET
    [ActionName(("Index"))]
    public async Task<IActionResult> UserList()
    {
        UserListResponseModel model = await _userService.GetUsersAsync(new UserListRequestModel());
        return View("UserList", model);
    }
}