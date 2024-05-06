using Data.Dto;
using Data.Models;

namespace XepDapAPI_1.Service.Interfaces
{
    public interface IOrderIService
    {
        public (Order, List<Order_Details>) Create(OrderDto orderDto);
    }
}
