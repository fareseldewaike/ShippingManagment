using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.DTO
{
    public class MerchantWithSpecialPricesDto
    {
        public string StoreName { get; set; }

        public string MerchantName { get; set; }
        public List<SpecialPriceDto> SpecialPrices { get; set; } = new List<SpecialPriceDto>();
    }
}
