using System.ComponentModel.DataAnnotations;

namespace Shipping.DTO
{
    public class GovernorateDTO
    {
         [Required]
        public string Name { get; set; }
        public List<CityDTO> Cities { get; set; } = new();

    }
}
