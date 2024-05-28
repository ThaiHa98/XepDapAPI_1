using Data.DBContext;
using Data.Dto;
using Data.Models;
using Microsoft.EntityFrameworkCore.Internal;
using XepDapAPI_1.Repository.Interface;
using static System.Net.Mime.MediaTypeNames;

namespace XepDapAPI_1.Repository.Repositorys
{
    public class Products_DetailRepository : IProducts_DrtailInterface
    {
        private readonly MyDB _dbContext;
        public Products_DetailRepository(MyDB dbContext)
        {
            _dbContext = dbContext;
        }

        public ProductDetailGetInfDto Getproducts_Detail(int productId)
        {
            var result = (from productDetail in _dbContext.Product_Details
                          join brand in _dbContext.Brands on productDetail.BrandId equals brand.Id
                          join product in _dbContext.Products on productDetail.ProductID equals product.Id
                          where productDetail.ProductID == productId // Sửa điều kiện ở đây
                          select new ProductDetailGetInfDto
                          {
                              Id = productDetail.Id,
                              ProductID = productDetail.ProductID,
                              Price = productDetail.Price,
                              ProductName = product.ProductName,
                              PriceHasDecreased = productDetail.PriceHasDecreased,
                              BrandName = brand.BrandName,
                              Imgage = productDetail.Imgage,
                              Weight = productDetail.Weight,
                              Other_Details = productDetail.Other_Details
                          }).FirstOrDefault();
            return result;
        }
    }
}
