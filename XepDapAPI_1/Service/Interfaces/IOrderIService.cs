using Data.Dto;
using Data.Models;

namespace XepDapAPI_1.Service.Interfaces
{
    public interface IOrderIService
    {
        List<Order> Create(OrderDto orderDto);
    }
}
