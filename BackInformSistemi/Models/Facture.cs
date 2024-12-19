using System.ComponentModel.DataAnnotations.Schema;

namespace BackInformSistemi.Models
{
    public class Facture
    {
        public int Id { get; set; }

        [ForeignKey(nameof(saleId))]
        public Sale Sale { get; set; }
        public int saleId { get; set; }
        
    
    }
}
