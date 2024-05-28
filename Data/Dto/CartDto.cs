using Data.Models.Enum;

namespace Data.Dto
{
    public class CartDto
    {
        public int UserId { get; set; }
        public List<int> ProductIDs { get; set; }

    }
}
