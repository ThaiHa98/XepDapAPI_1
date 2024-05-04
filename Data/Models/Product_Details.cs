using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class Product_Details
    {
        [Key]
        public int Id { get; set; }
        public int ProductID { get; set; }
        public string Brand { get; set; } // thương hiệu
        public string Origin { get; set; } //Xuất xứ
        public float Weight { get; set; } //Trọng lượng
        public string Other_Details { get; set; } //thông tin chi tiết
    }
}
