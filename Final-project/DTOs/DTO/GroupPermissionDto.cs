using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.DTO
{
    public class GroupPermissionDto
    {
        public string Name { get; set; }  
        public List<PermissionDto> Permissions { get; set; }  

    }
}
