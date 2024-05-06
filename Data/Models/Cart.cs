using Data.Models.Enum;
using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProductID { get; set; }
        public string ProducName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Image { get; set; }
        public DateTime Create { get; set; }
        public StatusCart Status { get; set; }

        public List<Order_Details> Items { get; set; }
    }
}
