﻿using Data.Models.Enum;

namespace Data.Dto
{
    public class OrderDto
    {
        public string UserID { get; set; }
        public string ShipName { get; set; }
        public string ShipAddress { get; set; }
        public string ShipEmail { get; set; }
        public string ShipPhone { get; set; }
        public List<int> Cart { get; set; }
    }
}
