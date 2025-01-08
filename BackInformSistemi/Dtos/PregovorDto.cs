using System.ComponentModel.DataAnnotations;

namespace BackInformSistemi.Dtos
{
    public class PregovorDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Ponuda je obavezna.")]
        [MinLength(5, ErrorMessage = "Ponuda mora imati najmanje 5 karaktera.")]
        public string offer { get; set; }

        public int status { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "PropertyId mora biti veći od 0.")]
        public int propertyId { get; set; }
    }
}
