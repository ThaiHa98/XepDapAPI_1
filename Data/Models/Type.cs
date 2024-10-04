using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class Type
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ProductType { get; set; }
        public DateTime DateCreate { get; set; }
    }
}
