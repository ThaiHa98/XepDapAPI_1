using Data.Models.Enum;

namespace Data.Dto
{
    public class GetCartInfDto
    {
        public int CartId { get; set; }
        public int ProductID { get; set; }
        public decimal PriceProduct { get; set; }
        public string ProducName { get; set; }
        public decimal TotalPrice { get; set; }
        public int Quantity { get; set; }
        public string Image { get; set; }
    }
}
