using Data.DBContext;
using Data.Dto;
using Data.Models;
using Data.Models.Enum;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mime;
using XepDapAPI_1.Repository.Interface;
using XepDapAPI_1.Service.Interfaces;
using static System.Net.Mime.MediaTypeNames;

namespace XepDapAPI_1.Service.Services
{
    public class OrderService : IOrderIService
    {
        private readonly MyDB _dbContext;
        private readonly ICartInterface _cartInterface;
        public OrderService(MyDB dbContext,ICartInterface cartInterface)
        {
            _cartInterface = cartInterface;
            _dbContext = dbContext;
        }
        public (Order, List<Order_Details>) Create([FromQuery]OrderDto orderDto)
        {
            try
            {
                if (orderDto == null)
                {
                    throw new ArgumentNullException(nameof(orderDto), "OrderDto cannot be null");
                }

                var user = _dbContext.Users.FirstOrDefault(x => x.Id == orderDto.UserID);
                if (user == null)
                {
                    throw new Exception("UserId not found");
                }

                var order = new Order
                {
                    No_ = AutomaticallyGenerateOrderNumbers(),
                    UserID = user.Id,
                    ShipName = orderDto.ShipName,
                    ShipAddress = orderDto.ShipAddress,
                    ShipEmail = orderDto.ShipEmail,
                    ShipPhone = orderDto.ShipPhone,
                    Status = StatusOrder.Pending
                };

                if(orderDto.Cart != null && orderDto.Cart.Any())
                {
                    var orderDetails = new List<Order_Details>();
                    foreach (var productId in orderDto.Cart)
                    {
                        var cart = _cartInterface.GetProducId(productId);
                        if (cart != null)
                        {
                            var orderDetail = new Order_Details
                            {
                                OrderID = order.No_,
                                ProductID = cart.ProductID,
                                ProductName = cart.ProducName,
                                Quantity = cart.Quantity,
                                PriceProduc = cart.PriceProduct,
                                TotalPrice = cart.TotalPrice,
                                Image = cart.Image,
                                CreatedDate = DateTime.Now
                            };
                            orderDetails.Add(orderDetail);
                        }
                        else
                        {
                            throw new Exception($"Product with ID {productId} not found.");
                        }
                    }
                    _dbContext.Orders.Add(order);
                    _dbContext.Order_Details.AddRange(orderDetails);

                    _dbContext.SaveChanges();
                    return (order, orderDetails);
                }
                else
                {
                    throw new Exception("Cart is empty.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("There is an error when creating an Order", ex);
            }
        }

        private static string AutomaticallyGenerateOrderNumbers()
        {
            Random random = new Random();
            int randomNumber = random.Next(0, 1000);//Sinh ra số ngẫy nhiên từ 1 đến 9999
            string formattedNumber = randomNumber.ToString("D4");//Định dạng số thành chuỗi với độ dài 4 ký tư
            return "OR-" + formattedNumber;
        }
    }
}
