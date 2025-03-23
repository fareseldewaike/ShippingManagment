using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Shipping.core.Models
{
    public class Employee:ApplicationUser
    {
        [ForeignKey("Group")]
        public int GroupId { get; set; }
        public virtual Group? Group { get; set; }
    }
}
