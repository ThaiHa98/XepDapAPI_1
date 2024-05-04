﻿using Data.Models.Enum;
using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class Brand
    {
        [Key]
        public int Id { get; set; }
        public string? BrandName { get; set; }
        public string? Origin { get; set; } //xuất xứ
        public DateTime Create { get; set; }
        public StatusBrand Status { get; set; }
    }
}
