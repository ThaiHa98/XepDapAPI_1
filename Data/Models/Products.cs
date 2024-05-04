using Data.Models.Enum;
using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class Products
    {
        [Key]
        public int Id { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public decimal PriceHasDecreased { get; set; }//giá đã giảm
        public string Description { get; set; }
        public int Quantity { get; set; }
        public string Image { get; set; }
        public DateTime Create { get; set; }
        public StatusProduct Status { get; set; }
    }
}
