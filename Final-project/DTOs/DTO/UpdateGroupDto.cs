using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.DTO
{
    public class UpdateGroupDto
    {
        public string Name { get; set; }
        public List<PermissionnDto> Permissions { get; set; }
    }
}
