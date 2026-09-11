using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using June2026.ChartApp.Models;
using System.Reflection.Emit;
using June2026.Domain.Features.ProductFeatures;
using June2026.Domain.Models;

namespace June2026.ChartApp.Controllers;

public class HomeController : Controller
{
    private readonly IProductService _productService;

    public HomeController(IProductService productService)
    {
        _productService = productService;
    }

    public async Task<IActionResult> IndexAsync()
    {
       var model = await _productService.GetAllProductsAsync();
        List<int> series = model.Products.Select(x => x.Quantity).ToList();
        List<string> labels = model.Products.Select(x => x.Name).ToList();
        ViewData["Series"] = series;
        ViewData["Labels"] = labels;
        return View();
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
