using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using June2026.Database.AppDbContextModels;
using June2026.ScalarTestWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace June2026.ScalarTestWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _db;
        public ProductController()
        {
            _db = new AppDbContext();
        }
        [HttpGet]
        public IActionResult GetProducts()
        {
            var productList = _db.TblProducts.Select(x => new ProductModel
            {
                id = x.Id,
                name = x.Name,
                price = x.Price,
                quantity = x.Quantity
            }).ToList();
            ProductListResponseModel model = new ProductListResponseModel
            {
                isSuccess = true,
                Message = "Product list retrieved successfully",
                Data = productList
            };
            return Ok(model);
        }

        [HttpGet("{id}")]
        public IActionResult GetProductById([FromRoute] ProductGetByIdRequestModel requestModel)
        {
            var item = _db.TblProducts
                        .Where(x => x.Id == requestModel.id)
                        .Select(x => new ProductModel
                        {
                            id = x.Id,
                            name = x.Name,
                            price = x.Price,
                            quantity = x.Quantity
                        }).FirstOrDefault();
            if (item is null)
            {
                return NotFound(new ProductGetByIdResponseModel
                {
                    isSuccess = false,
                    Message = "Product is not found"
                });
            }
            ProductGetByIdResponseModel model = new ProductGetByIdResponseModel
            {
                isSuccess = true,
                Message = "Product retrieved successfully",
                Data = item
            };
            return Ok(model);
        }
        [HttpPost]
        public IActionResult CreateProduct(CreateProductRequestModel requestModel)
        {
            TblProduct product = new TblProduct
            {
                Name = requestModel.name,
                Price = requestModel.price,
                Quantity = requestModel.quantity
            };
            _db.TblProducts.Add(product);
            int result = _db.SaveChanges();
            CreateProductResponseModel model = new CreateProductResponseModel
            {
                isSuccess = true,
                Message = "Created the product successfully",
                Data = new ProductModel
                {
                    id = product.Id,
                    name = product.Name,
                    price = product.Price,
                    quantity = product.Quantity
                }
            };
            return Ok(model);
        }
        [HttpPatch("{id}")]
        public IActionResult UpdateProduct(int id,  UpdateProductRequestModel requestModel)
        {
            var item = _db.TblProducts
                        .Where(x => x.Id == id)
                        .Select(x => new ProductModel
                        {
                            id = x.Id,
                            name = x.Name,
                            price = x.Price,
                            quantity = x.Quantity
                        }).FirstOrDefault();
            if (item is null)
            {
                return NotFound(new UpdateProductResponseModel
                {
                    isSuccess = false,
                    Message = "Product is not found"
                });
            }
            if(!String.IsNullOrEmpty(requestModel.name))
            {
                item.name = requestModel.name;
            }
            if(requestModel.price.HasValue) 
            {
                item.price = requestModel.price.Value;
            }
            if (requestModel.quantity.HasValue)
            {
                item.quantity = requestModel.quantity.Value;
            }
            int result = _db.SaveChanges();
            UpdateProductResponseModel model = new UpdateProductResponseModel
            {
                isSuccess = true,
                Message = "Update the product successfully",
                Data = new ProductModel
                {
                    id = item.id,
                    name = item.name,
                    price = item.price,
                    quantity = item.quantity
                }
            };
            return Ok(model);

        }
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct([FromRoute] DeleteProductRequestModel requestModel)
        {
            var item = _db.TblProducts.FirstOrDefault(x => x.Id == requestModel.id);
            if(item is null)
            {
                return NotFound("Product is not found");
            }
            _db.Remove(item);
            int result =_db.SaveChanges();
            DeleteProductResponseModel model = new DeleteProductResponseModel
            {
                isSuccess = result > 0,
                Message = result > 0 ? "Delete the product successfully" : "Failed to delete"
            };
            return Ok(model);
        }
    }
    }
