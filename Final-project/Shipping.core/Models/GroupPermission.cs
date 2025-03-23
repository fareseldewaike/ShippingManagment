using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.core.Models
{
    public class GroupPermission
    {
        public int id { get; set; }
        public string Action { get; set; }
        public int GroupId { get; set; }

        public virtual Group? Group { get; set; }
        public int PermissionId { get; set; }
        public virtual Permission? Permission { get; set; }
    }
}
