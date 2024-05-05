using Data.Models.Enum;

namespace Data.Dto
{
    public class ProductPriceHasDecreasedInfDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public decimal PriceHasDecreased { get; set; }//giá đã giảm
        public string Description { get; set; }
        public string Image { get; set; }
    }
}
