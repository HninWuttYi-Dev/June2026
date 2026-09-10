using June2026.Domain.Features.UserFeatures;
using Microsoft.AspNetCore.Identity;
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
    [ActionName("Index")]
    public async Task<IActionResult> UserList()
    {
        UserListResponseModel model = await _userService.GetUsersAsync(new UserListRequestModel());
        return View("UserList", model);
    }
    [ActionName("Create")]
    public IActionResult UserCreate()
    {
        List<UserRoleModel> Roles = new List<UserRoleModel>
        {
            new UserRoleModel("Admin", "Administrator"),
            new UserRoleModel("User",  "Regular User"),
            new UserRoleModel("Guest", "GuestUser")
        }; 
        // ViewBag.Roles = Roles;
        ViewData["Roles"] = Roles;
        return View("UserCreate");
    }
    [HttpPost]
    [ActionName("Save")]
    public async Task<IActionResult> UserSave(UserCreateRequestModel requestModel)
    {
        bool isSuccess = false;
        string message = string.Empty;
        if(string.IsNullOrEmpty(requestModel.Username))
        {
            message = "Invalid Username";
            TempData["isSuccess"] = isSuccess;
            TempData["Message"] = message;
            return Redirect("/User/Create");
        }
        if(string.IsNullOrEmpty(requestModel.Password))
        {
            message = "Invalid Password";
            TempData["isSuccess"] = isSuccess;
            TempData["Message"] = message;
            return Redirect("/User/Create");
        }
        if (string.IsNullOrEmpty(requestModel.RoleCode) || requestModel.RoleCode == "0")
        {
            message = "Invalid Role";
            TempData["isSuccess"] = isSuccess;
            TempData["Message"] = message;
            return Redirect("/User/Create");
        }
        UserCreateResponseModel model = await _userService.CreateUserAsync(requestModel);
        // ViewBag.isSuccess = model.isSuccess;
        // ViewData["isSuccess"] = model.isSuccess;
        // TempData["isSuccess"] = model.isSuccess; //redirect

        // ViewBag.Message = model.Message;
        // ViewData["Message"] = model.Message;
        // TempData["Message"]= model.Message; //redirect
        isSuccess = model.isSuccess;
        message = model.Message;
        TempData["isSuccess"] = isSuccess;
        TempData["Message"] = message;
        return Redirect("/User/Index");
    }
    //user/edit/1
    [ActionName("Edit")]
    public async Task<IActionResult> UserEdit(int id)
    {
       UserEditResponseModel model = await _userService.GetUserAsync(new UserEditRequestModel() { UserId = id });
        return View("UserEdit", model);
    }
    [HttpPost]
    [ActionName("Update")]
    public async Task<IActionResult> UserUpdateAsync(int id, UserPatchRequestModel requestModel)
    {
        requestModel.UserId = id;
        UserPatchResponseModel model = await _userService.UpdateUserAsync(requestModel);
        return Redirect("/User/Index");
    }
    [ActionName("Delete")]
     public async Task<IActionResult> UserDeleteAsync(int id)
    {
        UserDeleteResponseModel model = await _userService.DeleteUserAsync(new UserDeleteRequestModel() {UserId = id});
        return Redirect("/User/Index");
    }
    public record UserRoleModel(string RoleCode, string RoleName);
    public record UserRoleModel2
    {
        string RoleCode {get; set;}
        string RoleName {get; set;}
    }
}