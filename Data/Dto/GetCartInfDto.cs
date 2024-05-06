using Data.Models.Enum;

namespace Data.Dto
{
    public class GetCartInfDto
    {
        public string ProducName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Image { get; set; }
    }
}
