using Data.Models;
using XepDapAPI_1.Repository.Interface;
using XepDapAPI_1.Service.Interfaces;

namespace XepDapAPI_1.Repository.Repositorys
{
    public class CartRepository : ICartInterface
    {
        public Cart GetCartItemByUser(int productId, int userId)
        {
            throw new NotImplementedException();
        }
    }
}
