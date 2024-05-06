using Data.DBContext;
using Data.Dto;
using Data.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Drawing;
using XepDapAPI_1.Repository.Interface;
using XepDapAPI_1.Service.Interfaces;
using static System.Net.Mime.MediaTypeNames;

namespace XepDapAPI_1.Service.Services
{
    public class ProductsService : IProductsIService
    {
        private readonly MyDB _dbContext;
        private readonly IProductsInterface _productsInterface;
        public ProductsService(MyDB dbContext,IProductsInterface productsInterface)
        {
            _productsInterface = productsInterface;
            _dbContext = dbContext;
        }
        public Products Create(ProductDto productsDto, IFormFile image)
        {
            try
            {
                var type = _dbContext.Types.FirstOrDefault(x => x.Id == productsDto.TypeId);
                if(type == null)
                {
                    throw new Exception("typeId not found");
                }
                var brand = _dbContext.Brands.FirstOrDefault(x => x.Id == productsDto.BrandId);
                if(brand == null)
                {
                    throw new Exception("brandId not found");
                }
                Products products = new Products
                {
                    ProductName = productsDto.ProductName,
                    Price = productsDto.Price,
                    PriceHasDecreased = productsDto.PriceHasDecreased,
                    Description = productsDto.Description,
                    Quantity = productsDto.Quantity,
                    Create = DateTime.Now,
                    TypeId = type.Id,
                    TypeName = type.Name,
                    BrandId = brand.Id,
                    brandName = brand.BrandName,
                };
                if (image != null && image.Length > 0)
                {
                    string imagePath = SaveProductImage(image);
                    products.Image = imagePath;
                    _dbContext.Products.Add(products);
                    _dbContext.SaveChanges();
                }
                return products;
            }
            catch (Exception ex) 
            {
                throw new Exception("There is an error when creating a Produc", ex);
            }
        }

        public bool Delete(int Id)
        {
            try
            {
                var delete = _dbContext.Products.FirstOrDefault(x => x.Id == Id);
                if(delete == null)
                {
                    throw new Exception("Id not found");
                }
                _dbContext.Products.Remove(delete);
                _dbContext.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                throw new Exception("There was an error while deleting the Product",ex);
            }
        }

        public List<object> GetBrandName(string keyword)
        {
            keyword = keyword.ToLower();
            List<object> result = new List<object>();

            List<ProductBrandInfDto> products = _productsInterface.GetAllBrandName(keyword);
            foreach (var item in products)
            {
                var productBrandInfo = new ProductBrandInfDto
                {
                    ProductName = item.ProductName,
                    Price = item.Price,
                    PriceHasDecreased = item.PriceHasDecreased,
                    Description = item.Description,
                    Image = item.Image,
                    BrandName = item.BrandName,
                };
                result.Add(productBrandInfo);
            }
            return result;
        }

        public List<object> GetPriceHasDecreased()
        {
            List<object> result = new List<object>();

            List<ProductPriceHasDecreasedInfDto> products = _productsInterface.SearchProductsByPriceHasDecreased();
            foreach (var item in products)
            {
                var productPriceHasDecreasedInfDto = new ProductPriceHasDecreasedInfDto
                {
                    Id = item.Id,
                    ProductName = item.ProductName,
                    Price = item.Price,
                    PriceHasDecreased = item.PriceHasDecreased,
                    Description = item.Description,
                    Image = item.Image
                };
                result.Add(productPriceHasDecreasedInfDto);
            }
            return result;
        }

        public List<object> GetTypeName(string keyword)
        {
            keyword = keyword.ToLower();
            List<object> resultType  = new List<object>();

            List<ProductTypeInfDto> products1 = _productsInterface.GetAllTypeName(keyword);
            foreach (var item in products1)
            {
                var productTypeInfo = new ProductTypeInfDto
                {
                    Id = item.Id,
                    ProductName = item.ProductName,
                    Price = item.Price,
                    PriceHasDecreased = item.PriceHasDecreased,
                    Description = item.Description,
                    Image = item.Image,
                    TypeName = item.TypeName,
                };
                resultType.Add(productTypeInfo);
            }
            return resultType;
        }

        public string Update(UpdateProductDto updateProductDto, IFormFile image)
        {
            try
            {
                var products = _dbContext.Products.FirstOrDefault(x => x.Id == updateProductDto.Id);
                 if(products == null)
                {
                    throw new Exception("productId not found");
                }
                products.Price = updateProductDto.Price;
                products.PriceHasDecreased = updateProductDto.PriceHasDecreased;
                products.Description = updateProductDto.Description;
                if (image != null && image.Length > 0)
                {
                    string imagePath = SaveProductImage(image);
                    products.Image = imagePath;
                }
                _dbContext.SaveChanges();
                return "Product updated successfully";
            }
            catch(Exception ex)
            {
                throw new Exception("An error occurred while updating Brand", ex);
            }
        }
        private string SaveProductImage(IFormFile image)
        {
            try
            {
                string currentDateFolder = DateTime.Now.ToString("dd-MM-yyyy");
                string imagesFolder = Path.Combine(@"C:\Users\XuanThai\Desktop\ImageXedap", "Prodycts_images", currentDateFolder);
                if (!Directory.Exists(imagesFolder))
                {
                    Directory.CreateDirectory(imagesFolder);
                }
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                string filePath = Path.Combine(imagesFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    image.CopyTo(stream);
                }
                return filePath;
            }
            catch (Exception e)
            {
                throw new Exception($"An error occurred while saving the image: {e.Message}");
            }
        }
    }
}
