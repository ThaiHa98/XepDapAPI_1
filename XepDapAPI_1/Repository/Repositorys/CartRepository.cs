using Data.DBContext;
using Data.Dto;
using Data.Models;
using XepDapAPI_1.Repository.Interface;
using XepDapAPI_1.Service.Interfaces;
using static System.Net.Mime.MediaTypeNames;

namespace XepDapAPI_1.Repository.Repositorys
{
    public class CartRepository : ICartInterface
    {
        private readonly MyDB _dbContext;
        public CartRepository(MyDB dbContext)
        {
            _dbContext = dbContext;
        }

        public List<GetCartInfDto> GetCartItemByUser(int userId)
        {
            return _dbContext.Carts
            .Where(x => x.UserId == userId)
            .OrderBy(x => x.UserId)
            .Select(x => new GetCartInfDto
            {
                CartId = x.Id,
                ProductID = x.ProductID,
                ProducName = x.ProducName,
                PriceProduct = x.PriceProduct,
                TotalPrice = x.TotalPrice,
                Quantity = x.Quantity,
                Image = x.Image
             })
             .ToList();
        }

        public Cart GetProducId(int productId)
        {
            return _dbContext.Carts.FirstOrDefault(x => x.ProductID == productId);
        }
    }
}
