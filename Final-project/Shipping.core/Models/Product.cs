using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.core.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public double Weight { get; set; }
        public bool isDeleted { get; set; } = false;

        [ForeignKey("Order")]
        public int? OrderId { get; set; }
        public virtual Order? Order { get; set; }
    }
}
