using Data.DBContext;
using Data.Dto;
using Data.Models;
using System.Drawing;
using XepDapAPI_1.Service.Interfaces;
using static System.Net.Mime.MediaTypeNames;

namespace XepDapAPI_1.Service.Services
{
    public class ProductsService : IProductsIService
    {
        private readonly MyDB _dbContext;
        public ProductsService(MyDB dbContext)
        {
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
                   BrandId = brand.Id,
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

        public bool Delete(Products products)
        {
            throw new NotImplementedException();
        }

        public string Update(Products products)
        {
            throw new NotImplementedException();
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
