using System.ComponentModel.DataAnnotations;

namespace Shipping.DTO
{
    public class GovernorateDTO
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public List<CityDTO> Cities { get; set; } = new();

    }
}
