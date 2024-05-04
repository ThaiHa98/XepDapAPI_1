using Data.Models.Enum;
using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
        [Required]
        public string? Address { get; set; }
        [Required]
        public string? City { get; set; }
        [Required]
        public string? Phone { get; set; }
        [Required]
        public string? DateOfBirth { get; set; }
        [Required]
        public DateTime Create { get; set; }
    }
}
