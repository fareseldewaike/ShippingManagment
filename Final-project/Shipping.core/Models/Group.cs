using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.core.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime Date { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; } = false;
        public virtual ICollection<GroupPermission> GroupPermissions { get; set; } = new List<GroupPermission>();
        public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}
