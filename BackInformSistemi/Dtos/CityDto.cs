using System.ComponentModel.DataAnnotations;

namespace BackInformSistemi.Dtos
{
    public class CityDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //public int xyz { get; set; }
        [Required(ErrorMessage ="Ime je obavezno uneti")]
        [StringLength(50, MinimumLength =2)]
        [RegularExpression(".*[a-zA-Z]+.*",ErrorMessage ="samo brojevi nisu podrzani")]
        public string Country { get; set; }
    }
}
