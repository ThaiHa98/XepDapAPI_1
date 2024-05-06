using Data.Dto;
using Data.Models;

namespace XepDapAPI_1.Service.Interfaces
{
    public interface ICartIService
    {
        public List<Cart> CrateBicycle(CartDto cartDto);
        string IncreaseQuantityShoppingCart(int UserId,int createProductId);
        object ReduceShoppingCart(int UserId,int createProductId);
        bool Delete(int Id);
        List<object> GetCart(int userId);
    }
}
