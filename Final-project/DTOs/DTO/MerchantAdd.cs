using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 

namespace DTOs.DTO
{
    public class MerchantAdd
    {
        [Required(ErrorMessage = "Name is required.")]
        public string MerchantName { get; set; }
        [Required(ErrorMessage = "Address is required.")]
        public string Address { get; set; }
        public string StoreName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        [Required(ErrorMessage = "PhoneNumber is required.")]
        [Phone(ErrorMessage = "Invalid phone number format.")]
        public string PhoneNumber { get; set; } = string.Empty;

        public double? PickUp { get; set; }

        public double? ReturnerPercent { get; set; }
        public int BranchId { get; set; }
        public int CityId { get; set; }
        public int GovernorateId { get; set; }

        public List<SpecialPricesAdd> SpecialPrices { get; set; }  
    }
}
