using Data.Dto;
using Data.Models;

namespace XepDapAPI_1.Repository.Interface
{
    public interface IProductsInterface
    {
        ICollection<Products> GetAllProducts();
        List<ProductBrandInfDto> GetAllBrandName(string keyword);
        List<ProductTypeInfDto> GetAllTypeName(string keyword);
        List<ProductPriceHasDecreasedInfDto> SearchProductsByPriceHasDecreased();
    }
}
