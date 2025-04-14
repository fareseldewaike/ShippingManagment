using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.DTO.Order
{
    public class GetOrderDTO
    {
        public int SerialNum { get; set; }
        public string ClientName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public double orderCost { get; set; }
        public string Governorate { get; set; }

        public string City { get; set; } = string.Empty;
        
        public DateTime Date { get; set; }
       
      
       

    }
}
