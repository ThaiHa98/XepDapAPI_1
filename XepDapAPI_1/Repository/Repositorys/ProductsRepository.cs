using Data.Models;
using XepDapAPI_1.Repository.Interface;

namespace XepDapAPI_1.Repository.Repositorys
{
    public class ProductsRepository : IProductsInterface
    {
        public List<Products> GetAll(string typeName)
        {
            throw new NotImplementedException();
        }

        public ICollection<Products> GetProducts(int productsID)
        {
            throw new NotImplementedException();
        }

        public List<Products> SearchProductsByPriceHasDecreased()
        {
            throw new NotImplementedException();
        }

        public List<Products> SearchProductsByTypeName(string typeName)
        {
            throw new NotImplementedException();
        }
    }
}
