using Data.Models;

namespace Data.Dto
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public decimal PriceHasDecreased { get; set; }//giá đã giảm
        public string Description { get; set; }
        public int Quantity { get; set; }
        public int BrandId { get; set; }
        public int TypeId { get; set; }
 
    }
}
