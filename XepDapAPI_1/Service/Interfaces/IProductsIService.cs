using Data.Models;

namespace XepDapAPI_1.Service.Interfaces
{
    public interface IProductsIService
    {
        public Products Create (Products products);
        string Update (Products products);
        bool Delete (Products products);
    }
}
