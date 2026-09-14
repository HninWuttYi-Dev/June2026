using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Core;
using June2026.Database.AppDbContextModels;
using June2026.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace June2026.Domain.Features.ProductFeatures
{


    public class ProductService : IProductService
    {
        private readonly AppDbContext _db;
        public ProductService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<ProductListResponseModel> GetAllProductsAsync(ProductListRequestModel requestModel)
        {
            try
            {
                var lst = await _db.TblProducts.ToListAsync();
                List<Product> products = new List<Product>();
                foreach (var item in lst)
                {
                    Product product = new Product
                    {
                        Name = item.Name,
                        Price = item.Price,
                        Quantity = item.Quantity,
                    };
                    products.Add(product);
                }
                return new ProductListResponseModel
                {
                    isSuccess = true,
                    Message = "Product list fetched successfully",
                    Products = products
                };
            }
            catch (Exception ex)
            {
                return new ProductListResponseModel
                {
                    isSuccess = false,
                    Message = ex.ToString()
                };
            }
        }

        public async Task<ProductEditResponseModel> GetProductByIdAsync(ProductEditRequestModel requestModel)
        {
            try
            {
                var item = await _db.TblProducts.FirstOrDefaultAsync(x => x.Id == requestModel.Id);

                return new ProductEditResponseModel
                {
                    isSuccess = true,
                    Message = "Product fetched successfully",
                    Id = item.Id,
                    Name = item.Name,
                    Price = item.Price,
                    Quantity = item.Quantity
                };
            }
            catch (Exception ex)
            {
                return new ProductEditResponseModel
                {
                    isSuccess = false,
                    Message = ex.ToString()
                };
            }
        }
        public async Task<ProductCreateResponseModel> CreateProductAsync(ProductCreateRequestModel requestModel)
        {
            try
            {
                if (requestModel.Price < 0)
                {
                    return new ProductCreateResponseModel
                    {
                        isSuccess = false,
                        Message = "Price must be greater than 0"
                    };
                }
                TblProduct product = new TblProduct
                {
                    Name = requestModel.Name,
                    Price = requestModel.Price,
                    Quantity = requestModel.Quantity
                };
                await _db.TblProducts.AddAsync(product);
                int result = await _db.SaveChangesAsync();
                return new ProductCreateResponseModel
                {
                    isSuccess = true,
                    Message = "Product is created successfully",
                    Id = product.Id
                };
            }
            catch (Exception ex)
            {

                return new ProductCreateResponseModel
                {
                    isSuccess = false,
                    Message = ex.ToString()

                };
            }
        }
        public async Task<ProductPatchResponseModel> UpdateProduct(ProductPatchRequestModel requestModel)
        {
            try
            {
                var item = await _db.TblProducts.FirstOrDefaultAsync(x => x.Id == requestModel.Id);
                if (item is null)
                {
                    return new ProductPatchResponseModel
                    {
                        isSuccess = false,
                        Message = "Product is not found"
                    };
                }
                if (!string.IsNullOrEmpty(requestModel.Name)) item.Name = requestModel.Name;
                if (requestModel.Price.HasValue) item.Price = requestModel.Price.Value;
                if (requestModel.Quantity.HasValue) item.Quantity = requestModel.Quantity.Value;
                int result = await _db.SaveChangesAsync();
                return new ProductPatchResponseModel
                {
                    isSuccess = true,
                    Message = "Product is updated successfully"
                };
            }
            catch (Exception ex)
            {
                return new ProductPatchResponseModel
                {
                    isSuccess = false,
                    Message = ex.ToString()
                };
            }
        }
        public async Task<ProductDeleteResponseModel> DeleteProductAsync(ProductDeleteRequestModel requestModel)
        {
            try
            {
                var item = await _db.TblProducts.FirstOrDefaultAsync(x => x.Id == requestModel.Id);
                if (item is null)
                {
                    return new ProductDeleteResponseModel
                    {
                        isSuccess = false,
                        Message = "Product is not found"
                    };
                }
                _db.Remove(item);
                await _db.SaveChangesAsync();
                return new ProductDeleteResponseModel
                {
                    isSuccess = true,
                    Message = "Product is deleted successfully"
                };
            }
            catch (Exception ex)
            {
                return new ProductDeleteResponseModel
                {
                    isSuccess = false,
                    Message = ex.ToString()
                };
            }
        }

        public Task<ProductListResponseModel> GetAllProductsAsync()
        {
            throw new NotImplementedException();
        }
    }

}