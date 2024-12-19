using System.ComponentModel.DataAnnotations.Schema;

namespace BackInformSistemi.Models
{
    public class Sale
    {
        public int Id { get; set; }
        public  string Date { get; set; }

        public  float Price { get; set; }
        [ForeignKey(nameof(sellerId))]
        public  User Seller{ get; set; }
        public int sellerId { get; set; }
        [ForeignKey(nameof(buyerId))]
        public User Buyer { get; set; }

        public int  buyerId { get; set; }

        [ForeignKey(nameof(agentId))]
        public User Agent { get; set; }

        public int agentId { get; set; }

        [ForeignKey(nameof(propertyId))]
        public Property Property { get; set; }
        public int propertyId { get; set; }
    }

}

