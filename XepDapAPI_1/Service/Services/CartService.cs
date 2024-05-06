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

        //Tăng số lượng trong giỏ hàng
        public string IncreaseQuantityShoppingCart(int UserId, int createProductId)
        {
            try
            {
                var cart = _dbContext.Carts.FirstOrDefault(x => x.UserId == UserId && x.ProducID == createProductId);
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
                var cart = _dbContext.Carts.FirstOrDefault(x => x.UserId == UserId && x.ProducID == createProductId);
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
