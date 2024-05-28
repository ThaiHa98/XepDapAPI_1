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

        public List<ProductGetAllInfDto> GetAllProducts()
        {
            return _dbContext.Products
                 .Select(x => new ProductGetAllInfDto
                 {
                     Id = x.Id,
                     ProductName = x.ProductName,
                     Price = x.Price,
                     PriceHasDecreased = x.PriceHasDecreased,
                     Image = x.Image,
                 })
                 .ToList();
        }

        public List<ProductTypeInfDto> GetAllTypeName(string keyword, int limit = 8)
        {
            keyword = keyword.ToLower();

            return _dbContext.Products
                .Where(x => x.TypeName.ToLower().Contains(keyword))
                .OrderBy(x => x.TypeName)
                .Take(limit) // Lấy ra tối đa 'limit' sản phẩm
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


        public Products GetProductsId(int Id)
        {
            return _dbContext.Products.FirstOrDefault(x => x.Id == Id);
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
