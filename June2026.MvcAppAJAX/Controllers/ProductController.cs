using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using June2026.Domain.Features.ProductFeatures;
using June2026.MvcAppAJAX.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace June2026.MvcAppAJAX.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IHubContext<RealTimeHub> _hubContext;
        public ProductController(IProductService productService, IHubContext<RealTimeHub> hubContext)
        {
            _productService = productService;
            _hubContext = hubContext;
        }

        [ActionName("Index")]
        public async Task<IActionResult> ProductListAsync()
        {
            ProductListResponseModel model = await _productService.GetAllProductsAsync(new ProductListRequestModel());
            model.Products ??= new List<Product>();
            ViewData["ProductCards"] = model.Products;
            return View("ProductList", model);
        }
        [ActionName("Create")]
        public IActionResult ProductCreate()
        {
            return View("ProductCreate");
        }
        [HttpPost]
        [ActionName("Save")]
        public async Task<IActionResult> ProductSaveAsync(ProductCreateRequestModel requestModel)
        {
            if (string.IsNullOrWhiteSpace(requestModel.Name))
            {
                // TempData["isSuccess"] = false;
                // TempData["Message"] = "Product's name is required";
                return Json(new ProductCreateResponseModel
                {
                    isSuccess = false,
                    Message = "Product's name is required"
                });
            }
            if (requestModel.Price <= 0)
            {
                return Json(new ProductCreateResponseModel
                {
                    isSuccess = false,
                    Message = "Price must be greater than zero"
                });
            }
            if (requestModel.Quantity < 0)
            {
                return Json(new ProductCreateResponseModel
                {
                    isSuccess = false,
                    Message = "Quantity must be greater or zero"
                });
            }
            ProductCreateResponseModel model = await _productService.CreateProductAsync(requestModel);
            if(model.isSuccess)
            {
                var lst = await _productService.GetAllProductsAsync(new ProductListRequestModel{});
                var labels =  lst.Products.Select(p=> p.Name).ToList();
                var data = lst.Products.Select(p=> p.Quantity).ToList();
                await _hubContext.Clients.All.SendAsync("ReceiveProductsUpdateEvent", labels, data);
            }
            return Json(model);
        }
        [ActionName("Edit")]
        public async Task<IActionResult> ProductEditAsync(int id)
        {
            ProductEditResponseModel model = await _productService.GetProductByIdAsync(
                            new ProductEditRequestModel { Id = id });
            if (!model.isSuccess)
            {
                TempData["isSuccess"] = false;
                TempData["Message"] = model.Message;
                return Redirect("/Product");
            }
            ViewData["Id"] = model.Id;
            ViewData["Name"] = model.Name;
            ViewData["Price"] = model.Price;
            ViewData["Quantity"] = model.Quantity;
            return View("ProductEdit", model);
        }
        [HttpPost]
        [ActionName("Update")]
        public async Task<IActionResult> ProductUpdateAsync(int id, ProductPatchRequestModel requestModel)
        {
            requestModel.Id = id;
            if (
            string.IsNullOrWhiteSpace(requestModel.Name)
           && requestModel.Price is null
           && requestModel.Quantity is null)
            {
                return Json(new ProductPatchResponseModel
                {
                    isSuccess = false,
                    Message = "Please update at least one field."
                });
            }
            var model = await _productService.UpdateProductAsync(requestModel);
            if (model.isSuccess)
            {
                var lst = await _productService.GetAllProductsAsync(new ProductListRequestModel { });
                var labels = lst.Products.Select(p => p.Name).ToList();
                var data = lst.Products.Select(p => p.Quantity).ToList();
                await _hubContext.Clients.All.SendAsync("ReceiveProductsUpdateEvent", labels, data);
            }
            return Json(model);
        }
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> ProductDeleteAsync(ProductDeleteRequestModel requestModel)
        {
            var model = await _productService.DeleteProductAsync(requestModel);
            return Json(model);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}