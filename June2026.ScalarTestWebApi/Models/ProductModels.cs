namespace June2026.ScalarTestWebApi.Models
{
    public class ProductModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public decimal price { get; set; }
        public int quantity { get; set; }
    }
    //get all products response model
    public class ProductListResponseModel
    {
        public Boolean isSuccess { get; set; }
        public String Message { get; set; }
        public List<ProductModel> Data { get; set; }
    }

    //product get by Id
    public class ProductGetByIdRequestModel
    {
        public int id { get; set; }
    }
    public class ProductGetByIdResponseModel
    {
        public Boolean isSuccess { get; set; }
        public String Message { get; set; }
        public ProductModel Data { get; set; }
    }
    //create new product request model
    public class CreateProductRequestModel
    {

        public string name { get; set; }
        public decimal price { get; set; }
        public int quantity { get; set; }
    }
    public class CreateProductResponseModel
    {
        public Boolean isSuccess { get; set; }
        public string Message { get; set; }
        public ProductModel Data { get; set; }
    }
    //update the product
    public class UpdateProductRequestModel
    {
        public string? name {get; set;}
        public decimal? price {get; set;}
        public int? quantity { get; set;}
    }
    public class UpdateProductResponseModel
    {
        public Boolean isSuccess {get; set;}
        public string Message {get; set;}
        public ProductModel Data {get; set;}
    }
    //delete product request model
    public class DeleteProductRequestModel
    {
        public int id {get;set;}
    }
    public class DeleteProductResponseModel
    {
        public bool isSuccess {get;set;}
        public string Message {get; set;}

    }
}