using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.DTO
{
    public class CreateGroupDto
    {
        public string Name { get; set; }
        public List<GroupPermissionInputDto> Permissions { get; set; }
    }
}
