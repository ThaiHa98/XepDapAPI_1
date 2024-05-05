using Data.DBContext;
using Data.Dto;
using Data.Models;
using Data.Models.Enum;
using XepDapAPI_1.Service.Interfaces;
using static System.Net.Mime.MediaTypeNames;

namespace XepDapAPI_1.Service.Services
{
    public class CartService : ICartIService
    {
        private readonly MyDB _dbContext;
        public CartService(MyDB dbContext)
        {
            _dbContext = dbContext;
        }
        public List<Cart> CrateBicycle(CartDto cartDto)
        {
            List<Cart> cartList = new List<Cart>();
            var user = _dbContext.Users.FirstOrDefault(x => x.Id == cartDto.UserId);
            if (user == null)
            {
                throw new Exception("UserID not found");
            }
            foreach (var productId in cartDto.ProducIDs)
            {
                var product = _dbContext.Products.FirstOrDefault(x => x.Id == productId);
                if (product == null)
                {
                    throw new Exception("Product ID not found");
                }
                var cart = _dbContext.Carts.FirstOrDefault(x => x.ProducID == productId && x.UserId == cartDto.UserId);
                if (cart != null)
                {
                    cart.Quantity += 1;
                    cart.Price = product.Price * cart.Quantity;
                }
                else
                {
                    Cart newCart = new Cart
                    {
                        UserId = user.Id,
                        ProducID = productId,
                        ProducName = product.ProductName,
                        Price = product.Price,
                        Quantity = 1,
                        Image = product.Image,
                        Create = DateTime.Now,
                        Status = StatusCart.Pending
                    };
                    _dbContext.Carts.Add(newCart);
                    cartList.Add(newCart);
                }
            }
            _dbContext.SaveChanges();
            return cartList;
        }



        public bool Delete(int UserId)
        {
            throw new NotImplementedException();
        }

        public string IncreaseQuantityShoppingCart(int UserId, int createProductId)
        {
            throw new NotImplementedException();
        }

        public object ReduceShoppingCart(int UserId, int createProductId)
        {
            throw new NotImplementedException();
        }
    }
}
