using Data.DBContext;
using Data.Dto;
using Data.Models;
using XepDapAPI_1.Repository.Interface;

namespace XepDapAPI_1.Repository.Repositorys
{
    public class ProductsRepository : IProductsInterface
    {
        private readonly MyDB _dbContext;
        public ProductsRepository(MyDB dbContext)
        {
            _dbContext = dbContext;
        }
        public List<ProductBrandInfDto> GetAllBrandName(string keyword)
        {
            return _dbContext.Products
                .OrderBy(x => x.brandName)
                .Select(x => new ProductBrandInfDto
                {
                    Id = x.Id,
                    ProductName = x.ProductName,
                    Price = x.Price,
                    PriceHasDecreased = x.PriceHasDecreased,
                    Image = x.Image,
                    BrandName = x.brandName,
                })
                .ToList();
        }

        public ICollection<Products> GetAllProducts()
        {
            return _dbContext.Products.ToList();
        }

        public List<ProductTypeInfDto> GetAllTypeName(string keyword)
        {
            return _dbContext.Products
                .OrderBy(x => x.TypeName)
                .Select(x => new ProductTypeInfDto
                {
                    Id = x.Id,
                    ProductName = x.ProductName,
                    Price = x.Price,
                    PriceHasDecreased = x.PriceHasDecreased,
                    Image = x.Image,
                    TypeName = x.TypeName
                })
                .ToList();
        }

        public List<ProductPriceHasDecreasedInfDto> SearchProductsByPriceHasDecreased()
        {
            return _dbContext.Products
                .OrderBy(x => x.PriceHasDecreased)
                .Select(x => new ProductPriceHasDecreasedInfDto
                {
                    Id = x.Id,
                    ProductName = x.ProductName,
                    Price = x.Price,
                    PriceHasDecreased = x.PriceHasDecreased,
                    Description = x.Description,
                    Image = x.Image
                })
                .ToList();
        }
    }
}
