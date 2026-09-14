using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using June2026.RealTimeNotiApp.Models;
using Microsoft.AspNetCore.SignalR;
using June2026.RealTimeNotiApp.Hubs;

namespace June2026.RealTimeNotiApp.Controllers;

public class HomeController : Controller
{
    private static int count = 0;
    private readonly IHubContext<NotificationHub> _hubContext;

    public HomeController(IHubContext<NotificationHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public IActionResult Index()
    {
        return View();
    }
     public async Task<IActionResult> CountAsync()
    {
        count++;
       await _hubContext.Clients.All.SendAsync("NotiEvent", count);
        return Redirect("/Home");
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
