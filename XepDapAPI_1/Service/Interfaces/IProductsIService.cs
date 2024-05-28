using Data.Dto;
using Data.Models;

namespace XepDapAPI_1.Service.Interfaces
{
    public interface IProductsIService
    {
        public Products Create(ProductDto productsDto, IFormFile image);
        string Update(UpdateProductDto updateProductDto, IFormFile image);
        bool Delete(int Id);
        List<Object> GetBrandName(string keyword);
        List<Object> GetTypeName(string keyword, int limit = 6);
        List<Object> GetPriceHasDecreased();
        byte[] GetProductImageBytes(string imagePath);
        List<ProductGetAllInfDto> GetProductsInPriceRange(int minPrice, int maxPrice);
    }
}
