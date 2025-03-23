using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.core.Models
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public double Price { get; set; }
        public double? Pickup { get; set; }
        public bool isDeleted { get; set; } = false;

        [ForeignKey("Governorate")]
        public int GovernorateId { get; set; }
        public virtual Governorate? Governorate { get; set; }
      public virtual ICollection<SpecialPrice> SpecialPrices { get; set; } = new List<SpecialPrice>();
       // public virtual ICollection<Order> Orders { get; set; } = new HashSet<Order>();

    }
}
