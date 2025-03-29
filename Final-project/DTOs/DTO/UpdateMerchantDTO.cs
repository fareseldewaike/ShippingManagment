using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.DTO
{
    public class UpdateMerchantDTO
    {
        [Required]
        public string StoreName { get; set; }
        [Required]
        public string Address { get; set; }
    }
}
