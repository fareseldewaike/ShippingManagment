using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 

namespace DTOs.DTO
{
    public class MerchantAdd
    {
        public string MerchantName { get; set; }
        public string UserName { get; set; }
        public string Address { get; set; }
        public string StoreName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public double? PickUp { get; set; }

        public double? ReturnerPercent { get; set; }
        public int BranchId { get; set; }


        public List<SpecialPricesAdd> Price = new List<SpecialPricesAdd>();
    }
}
