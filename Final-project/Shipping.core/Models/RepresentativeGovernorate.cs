using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.core.Models
{
    public class RepresentativeGovernorate
    {
        public int Id { get; set; }

        [ForeignKey("Governorate")]
        public int GovernorateId { get; set; }
        public virtual Governorate? Governorate { get; set; }

        [ForeignKey("Representative")]
        public string RepresentativeId { get; set; } = string.Empty;
        public virtual Representative? Representative { get; set; }


    }
}
