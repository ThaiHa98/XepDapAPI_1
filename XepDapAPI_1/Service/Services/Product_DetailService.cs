using Data.DBContext;
using Data.Dto;
using Data.Models;
using XepDapAPI_1.Service.Interfaces;
using static System.Net.Mime.MediaTypeNames;

namespace XepDapAPI_1.Service.Services
{
    public class Product_DetailService : IProduct_DetailIService
    {
        private readonly MyDB _dbContext;
        public Product_DetailService(MyDB dbContext)
        {
            _dbContext = dbContext;
        }
        public Product_Details Create(Product_DetailDto product_DetailDto)
        {
            try
            {
                
                var products = _dbContext.Products.SingleOrDefault(x => x.Id == product_DetailDto.ProductID);

                if (products == null)
                {
                    throw new Exception("Product not found");
                }
                Product_Details product_Details = new Product_Details
                {
                    ProductID = products.Id,
                    BrandId = products.BrandId,
                    Price = products.Price,
                    PriceHasDecreased = products.PriceHasDecreased,
                    Imgage = products.Image,
                    Weight = product_DetailDto.Weight,
                    Other_Details = product_DetailDto.Other_Details,
                };
                _dbContext.Product_Details.Add(product_Details);
                _dbContext.SaveChanges();
                return product_Details;
            }
            catch (Exception ex)
            {
               throw new Exception("There is an error when creating a Product_Detail",ex);
            }
        }

        public bool Delete(int Id)
        {
            try
            {
                var query = _dbContext.Product_Details.FirstOrDefault(x => x.Id == Id);
                if(query == null)
                {
                    throw new Exception("Product_DetailsId not found");
                }
                _dbContext.Remove(query);
                _dbContext.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                throw new Exception("There was an error while deleting the Product_Detail", ex);
            }
        }

        public string Update(UpdateProduct_DetailsDto updateProduct_DetailsDto)
        {
            try
            {
                var product_Details = _dbContext.Product_Details.FirstOrDefault(x => x.Id == updateProduct_DetailsDto.Id);
                if(product_Details == null)
                {
                    throw new Exception("Product_DetailId not found");
                }
                product_Details.Weight = updateProduct_DetailsDto.Weight;
                product_Details.Other_Details = updateProduct_DetailsDto.Other_Details;
                _dbContext.Product_Details.Update(product_Details);
                _dbContext.SaveChanges();
                return "Update Successfully";
            }
            catch(Exception ex)
            {
                throw new Exception("An error occurred while updating Product_Details", ex);
            }
        }
    }
}
