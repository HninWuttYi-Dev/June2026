using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using June2026.MvcApp.Models;

namespace June2026.MvcApp.Controllers;

public class HomeController : Controller
{
    [ActionName(("Index"))]
    public IActionResult HomePage()
    {
        return View("HomePage");
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
