using System.ComponentModel.DataAnnotations;

namespace BackInformSistemi.Models
{
    public class FurnishingType : BaseEntity
    {
    
        [Required]
        public string Name { get; set; }
    }
}