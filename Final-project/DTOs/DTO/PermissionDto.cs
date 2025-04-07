using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.DTO
{
    public class PermissionDto
    {
        public string PermissionName { get; set; }  
        public List<string> Actions { get; set; }
    }
}
