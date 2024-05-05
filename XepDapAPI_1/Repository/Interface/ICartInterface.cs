using Data.Models;

namespace XepDapAPI_1.Repository.Interface
{
    public interface ICartInterface
    {
        Cart GetCartItemByUser(int productId, int userId);
    }
}
