﻿using Data.Models.Enum;
using System.ComponentModel.DataAnnotations;

namespace Data.Dto
{
    public class GetViewUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public string DateOfBirth { get; set; }
    }
}
