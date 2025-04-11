using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.core.Models
{
    public class SpecialPrice
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public Boolean Isdeleted { get; set; }=false;
        [ForeignKey("City")]
        public int CityId { get; set; }
        public virtual City? City { get; set; }

        [ForeignKey("Merchant")]
        public string MerchentId { get; set; }
        public virtual Merchant? Merchant { get; set; }
        [ForeignKey("Governorate")]
        public int GovernorateId { get; set; }
        public virtual Governorate? Governorate { get; set; }
    }
}
