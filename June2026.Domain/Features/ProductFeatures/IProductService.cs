using June2026.Domain.Models;

namespace June2026.Domain.Features.ProductFeatures
{
    public interface IProductService
    {
        Task<ProductCreateResponseModel> CreateProductAsync(ProductCreateRequestModel requestModel);
        Task<ProductDeleteResponseModel> DeleteProductAsync(ProductDeleteRequestModel requestModel);
        Task<ProductListResponseModel> GetAllProductsAsync(ProductListRequestModel requestModel);
        Task<ProductListResponseModel> GetAllProductsAsync();
        Task<ProductEditResponseModel> GetProductByIdAsync(ProductEditRequestModel requestModel);
        Task<ProductPatchResponseModel> UpdateProductAsync(ProductPatchRequestModel requestModel);
    }

}