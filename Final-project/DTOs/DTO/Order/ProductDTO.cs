using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.DTO.Order
{
    public class ProductDTO
    {
        public string Name { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public double Weight { get; set; }
    
    }
}
