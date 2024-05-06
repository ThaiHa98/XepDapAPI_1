using Data.Models.Enum;
using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public string OrderId { get; set; }
        public string UserID { get; set; }
        public string ShipName { get; set; }
        public string ShipAddress { get; set; }
        public string ShipEmail { get; set; }
        public string ShipPhone { get; set; }
        public StatusOrder Status { get; set; }

        public List<Order_Details> OrderDetails { get; set; }

    }
}
