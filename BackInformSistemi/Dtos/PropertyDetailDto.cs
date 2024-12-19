﻿using BackInformSistemi.Dtos;
using BackInformSistemi.Models;

namespace BackInformSistemi.Controllers
{
    internal class PropertyDetailDto : PropertyListDto
    {
        public int CarpetArea { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public int CityId { get; set; }

        public City City { get; set; }
        public int FloorNo { get; set; }
        public int TotalFlors { get; set; }
        public bool ReadyToMove { get; set; }
        public string MainEntrance { get; set; }
        public int Security { get; set; }
        public bool Gated { get; set; }
        public int Maintenance { get; set; }
        
        public int Age { get; set; }
        public string Description { get; set; }

        public User User { get; set; }

    }
}