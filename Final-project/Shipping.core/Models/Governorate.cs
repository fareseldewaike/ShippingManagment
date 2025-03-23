using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.core.Models
{
    public class Governorate
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsDeleted { get; set; } = false;

        public virtual ICollection<City> Cities { get; set; } = new List<City>();
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

        public ICollection<RepresentativeGovernorate> RepresentativeGovernorates = new List<RepresentativeGovernorate>();
        public virtual ICollection<SpecialPrice> SpecialPrices { get; set; } = new List<SpecialPrice>();
    }
}
