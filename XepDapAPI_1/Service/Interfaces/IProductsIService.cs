using Data.Dto;
using Data.Models;

namespace XepDapAPI_1.Service.Interfaces
{
    public interface IProductsIService
    {
        public Products Create (ProductDto productsDto, IFormFile image);
        string Update (Products products);
        bool Delete (Products products);
    }
}
