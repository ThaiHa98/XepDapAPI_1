using Data.Dto;
using Data.Models;

namespace XepDapAPI_1.Repository.Interface
{
    public interface ICartInterface
    {
        List<GetCartInfDto> GetCartItemByUser(int userId);
        Cart GetProducId(int productId);
    }
}
