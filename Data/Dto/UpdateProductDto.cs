using Data.Models.Enum;

namespace Data.Dto
{
    public class UpdateProductDto
    {
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public decimal PriceHasDecreased { get; set; }//giá đã giảm
        public string Description { get; set; }
        public int Quantity { get; set; }
        public IFormFile? Image { get; set; }
        public DateTime? Create { get; set; }
        public StatusProduct Status { get; set; }
    }
}
