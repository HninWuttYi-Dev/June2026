using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using June2026.Domain.Features.ProductFeatures;
using June2026.Domain.Models;
using June2026.RealTimeNotiApp.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace June2026.RealTimeNotiApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IHubContext<NotificationHub> _hubContext;
        public ProductController(IProductService productService, IHubContext<NotificationHub> hubContext)
        {
            _productService = productService;
            _hubContext = hubContext;
        }

        [ActionName("Index")]
        public async Task<IActionResult> ProductListAsync()
        {
            var model = await _productService.GetAllProductsAsync(new ProductListRequestModel());
            return View("ProductList", model);
        }
        [ActionName("Create")]
        public IActionResult ProductCreate()
        {
            return View("ProductCreate");
        }

        [HttpPost]
        [ActionName("Save")]
        public async Task<IActionResult> ProductSaveAsync([FromForm] ProductCreateRequestModel requestModel)
        {
            if (string.IsNullOrEmpty(requestModel.Name))
            {
                return Json(new { isSuccess = false, message = "Product Name is required" });
            }
            if (requestModel.Price <= 0)
            {
                return Json(new { isSuccess = false, message = "Price must be greater than zero" });
            }
            if (requestModel.Quantity <= 0)
            {
                return Json(new { isSuccess = false, message = "Quantity must be greater than zero" });
            }

            ProductCreateResponseModel model = await _productService.CreateProductAsync(requestModel);

            if (model.isSuccess)
            {
                await _hubContext.Clients.All.SendAsync("NewProductAdded", new
                {
                    Name = requestModel.Name,
                    Price = requestModel.Price,
                    Quantity = requestModel.Quantity
                });
            }

            return Json(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}