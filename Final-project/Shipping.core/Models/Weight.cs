using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.core.Models
{
    public class Weight
    {
        public int Id { get; set; }
        public double DefaultWeight { get; set; } = 10;
        public double AdditionalPrice { get; set; } = 20;
    }
}
