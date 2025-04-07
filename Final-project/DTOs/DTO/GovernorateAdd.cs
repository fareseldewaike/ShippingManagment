using Shipping.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.DTO
{
    public class GovernorateAdd
    {
        public string Name { get; set; }
        public List<CityAdd> Cities { get; set; } = new();
    }
}
