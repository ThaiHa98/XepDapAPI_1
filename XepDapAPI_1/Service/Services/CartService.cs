using Data.DBContext;
using Data.Dto;
using Data.Models;
using Data.Models.Enum;
using System.Dynamic;
using XepDapAPI_1.Repository.Interface;
using XepDapAPI_1.Service.Interfaces;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;
using static System.Net.Mime.MediaTypeNames;

namespace XepDapAPI_1.Service.Services
{
    public class CartService : ICartIService
    {
        private readonly MyDB _dbContext;
        private readonly ICartInterface _cartInterface;
        public CartService(MyDB dbContext, ICartInterface cartInterface)
        {
            _cartInterface = cartInterface;
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
                var cart = _dbContext.Carts.FirstOrDefault(x => x.ProductID == productId && x.UserId == cartDto.UserId);
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
                        ProductID = productId,
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

        public bool Delete(int Id)
        {
            try
            {
                var query = _dbContext.Carts.FirstOrDefault(x => x.Id == Id);
                if (query != null)
                {
                    throw new Exception("Id not found");
                }
                _dbContext.Carts.Remove(query);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while Delete the Cart quantity :{ex.Message}");
            }
        }

        public List<object> GetCart(int userId)
        {
            List<object> result = new List<object>();
            var cart = _dbContext.Carts.FirstOrDefault(x => x.UserId == userId);
            if (cart == null)
            {
                throw new Exception("cartId not found");
            }
            List<GetCartInfDto> cart1 = _cartInterface.GetCartItemByUser(userId);
            foreach (var item in cart1)
            {
                var caerInfo = new GetCartInfDto
                {
                    ProducName = item.ProducName,
                    Price = item.Price,
                    Quantity = item.Quantity,
                    Image = item.Image,
                };
                result.Add(caerInfo);
            }
            return result;
        }

        //Tăng số lượng trong giỏ hàng
        public string IncreaseQuantityShoppingCart(int UserId, int createProductId)
        {
            try
            {
                var cart = _dbContext.Carts.FirstOrDefault(x => x.UserId == UserId && x.ProductID == createProductId);
                if (cart == null) 
                {
                    throw new Exception("UserId & ProducId not found");
                }
                var product = _dbContext.Products.FirstOrDefault(x => x.Id == createProductId);
                if(product == null)
                {
                    throw new Exception("ProducId not found");
                }
                cart.Quantity += 1;
                cart.Price = product.Price * cart.Quantity;
                _dbContext.SaveChanges();
                return "Update Successfully";
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while updating the Cart quantity :{ex.Message}");
            }
        }

        //giảm số lượng trong giỏ hàng
        public object ReduceShoppingCart(int UserId, int createProductId)
        {
            try
            {
                var cart = _dbContext.Carts.FirstOrDefault(x => x.UserId == UserId && x.ProductID == createProductId);
                if(cart == null)
                {
                    throw new Exception("UserId & ProducId not found");
                }
                var product = _dbContext.Products.FirstOrDefault(x => x.Id == createProductId);
                if (product == null)
                {
                    throw new Exception("ProducId not found");
                }
                cart.Quantity -= 1;
                cart.Price = product.Price * cart.Quantity;
                if(cart.Quantity <= 0)
                {
                    _dbContext.Remove(cart);
                    return "Shopping cart item removed successfully";
                }
                _dbContext.SaveChanges();
                return "ReduceShoppingCart Successfully";
            }
            catch(Exception ex)
            {
                throw new Exception($"An error occurred while updating the Cart quantity :{ex.Message}");
            }
        }
    }
}
