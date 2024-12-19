﻿using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackInformSistemi.Models
{
    public class Pregovor
    {
        public int Id { get; set; }
        public int status { get; set; }

        public string offer { get; set; }

        public string date { get; set; }
        [ForeignKey(nameof(menagerId))]
        public User Menager { get; set; }
        public int menagerId { get; set; }

        [ForeignKey(nameof(buyerId))]
        public User Buyer { get; set; }
        public int buyerId {get; set;}

        [ForeignKey(nameof(agentId))]
        public User Agent { get; set; }
        public int agentId { get; set; }

        [ForeignKey(nameof(propertyId))]
        public Property Property { get; set; }
        public int propertyId { get; set; }

    }
}