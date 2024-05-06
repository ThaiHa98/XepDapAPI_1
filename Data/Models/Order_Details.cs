using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class Order_Details
    {
        [Key]
        public int Id { get; set; }
        public string OrderID { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
