using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.DTO.Order
{
    public class ReportDTO
    {
        public int SerialNumber { get; set; }
        public DateTime Date { get; set; }
        public string Governorate { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public double OrderCost { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public string ClientName { get; set; } = string.Empty;
        public string MerchantName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;

        public double paidAmount { get; set; }

        public double shippingCost { get; set; }
        public double paidshippingamount { get; set; }

        public double companyAmount { get; set; }

    }
}
