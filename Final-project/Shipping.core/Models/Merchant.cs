using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.core.Models
{
    public class Merchant: ApplicationUser
    {
        public string? StoreName { get; set; }

        public double? PickUp { get; set; }

        public double? ReturnerPercent { get; set; }

        [ForeignKey("City")]
        public int? CityId { get; set; }

        public virtual City? City { get; set; }


        [ForeignKey("Governorate")]
        public int? GovernorateId { get; set; }
        public virtual Governorate? Governorate { get; set; }

        public ICollection<SpecialPrice> SpecialPrices = new HashSet<SpecialPrice>();

    }
}
