using Data.Models.Enum;

namespace Data.Dto
{
    public class GetCartInfDto
    {
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public decimal PriceProduct { get; set; }
        public string ProductName { get; set; }
        public decimal TotalPrice { get; set; }
        public int Quantity { get; set; }
        public string Image { get; set; }
    }
}
