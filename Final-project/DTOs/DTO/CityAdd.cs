using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.DTO
{
    public class CityAdd
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public double? Pickup { get; set; }

        public int GovernorateId { get; set; }
    }
}
