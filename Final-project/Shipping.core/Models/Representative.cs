using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.core.Models
{
    public enum AmountType
    {
        Percent,
        Fixed
    }

    public class Representative: ApplicationUser
    {

        public double? Amount { get; set; }
        public AmountType? Type { get; set; }

        public ICollection<RepresentativeGovernorate> RepresentativeGovernorate = new List<RepresentativeGovernorate>();
    }
}
