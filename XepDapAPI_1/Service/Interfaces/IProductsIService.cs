using Data.Dto;
using Data.Models;

namespace XepDapAPI_1.Service.Interfaces
{
    public interface IProductsIService
    {
        Task<Products> Create(ProductDto productsDto);
        string Update(UpdateProductDto updateProductDto, IFormFile image);
        bool Delete(int Id);
        List<Object> GetBrandName(string keyword);
        List<Object> GetTypeName(string keyword, int limit = 6);
        List<Object> GetPriceHasDecreased();
        byte[] GetProductImageBytes(string imagePath);
        List<ProductGetAllInfPriceDto> GetProductsWithinPriceRangeAndBrand(decimal minPrice, decimal maxPrice, string brandsName);
        List<Product_detail> GetProductsByNameAndColor(string productName, string? color);
    }
}
