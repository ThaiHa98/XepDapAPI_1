using Data.DBContext;
using Data.Dto;
using Data.Models;
using System.Net;
using XepDapAPI_1.Service.Interfaces;

namespace XepDapAPI_1.Service.Services
{
    public class OrderService : IOrderIService
    {
        private readonly MyDB _dbContext;
        public OrderService(MyDB dbContext)
        {
            _dbContext = dbContext;
        }
        public List<Order> Create(OrderDto orderDto)
        {
            try
            {
                if(orderDto == null)
                {
                    throw new ArgumentNullException(nameof(orderDto), "OrderDto cannot be null");
                }
                var order = new Order
                {
                    OrderId = AutomaticallyGenerateOrderNumbers(),
                    UserID = orderDto.UserID,
                    ShipName = orderDto.ShipName,
                    ShipAddress = orderDto.ShipAddress,
                    ShipEmail = orderDto.ShipEmail,
                    ShipPhone = orderDto.ShipPhone,
                };
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
