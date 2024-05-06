using Data.Models.Enum;

namespace Data.Dto
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string OrderId { get; set; }
        public string UserID { get; set; }
        public string ShipName { get; set; }
        public string ShipAddress { get; set; }
        public string ShipEmail { get; set; }
        public string ShipPhone { get; set; }
        //-------------------------------------
        public string OrderID { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
