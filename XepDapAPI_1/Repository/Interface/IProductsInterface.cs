using Data.Models;

namespace XepDapAPI_1.Repository.Interface
{
    public interface IProductsInterface
    {
        ICollection<Products> GetProducts(int productsID);
        List<Products> GetAll(string typeName);
        List<Products> SearchProductsByTypeName(string typeName);
        List<Products> SearchProductsByPriceHasDecreased();
    }
}
