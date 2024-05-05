using Data.DBContext;
using Data.Models;
using XepDapAPI_1.Repository.Interface;

namespace XepDapAPI_1.Repository.Repositorys
{
    public class Products_DetailRepository : IProducts_DrtailInterface
    {
        private readonly MyDB _dbContext;
        public Products_DetailRepository(MyDB dbContext)
        {
            _dbContext = dbContext;
        }

        public Product_Details Getproducts_Detail(int Id)
        {
            try
            {
                if (Id <= 0)
                {
                    throw new ArgumentException("Invalid id", nameof(Id));
                }
                var query = _dbContext.Product_Details.FirstOrDefault(x => x.Id == Id);
                return query;
            }
            catch(Exception ex)
            {
                throw new ArgumentException("Error in Getproducts_Detail method", ex);
            }
        }
    }
}
