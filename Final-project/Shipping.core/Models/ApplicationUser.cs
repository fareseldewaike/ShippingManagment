using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;




namespace Shipping.core.Models
{
    public class ApplicationUser: IdentityUser
    {
        [Required]
        public string Address { get; set; } = string.Empty;


        [Required]
        [ForeignKey("branch")]
        public int BranchId { get; set; }
        public virtual Branch? branch { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;
        public bool IsDeleted { get; set; } = false;

    }
}
