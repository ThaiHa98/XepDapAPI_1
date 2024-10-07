using Data.Models.Enum;

namespace Data.Dto
{
    public class GetViewPurchasedProducts
    {
        public string No_ { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string ShipName { get; set; }
        public string ShipAddress { get; set; }
        public string ShipEmail { get; set; }
        public string ShipPhone { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal PriceProduc { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public string Image { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
