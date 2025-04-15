using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Required]
        public double? Amount { get; set; }
        [Required]
        public AmountType? Type { get; set; }

        public ICollection<RepresentativeGovernorate> RepresentativeGovernorate = new List<RepresentativeGovernorate>();
    }
}
