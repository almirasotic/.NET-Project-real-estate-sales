﻿using BackInformSistemi.Models;

namespace BackInformSistemi.Dtos
{
    public class PropertyListDto
    {
        public int Id { get; set; }
        public int SellRent { get; set; }
        public string Name { get; set; }
        public string PropertyType { get; set; }
        public string FurnishingType { get; set; }

        public int Price { get; set; }

        public int BHK { get; set; }

        public int BuiltAre { get; set; }

        public string City { get; set; }

        public string Country { get; set; }
        public bool ReadyToMove { get; set; }
        public DateTime EstPossessionOn { get; set; }
        public User User { get; set; }
        public string ImagePath { get; set; }
    }
}
