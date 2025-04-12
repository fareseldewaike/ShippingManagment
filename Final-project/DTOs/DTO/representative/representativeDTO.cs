using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.DTO.representative
{
    public class representativeDTO
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        public List<governorateDTORep> governorates { get; set; }
        public string brancheName { get; set; }

    }
    public class governorateDTORep
    {
        public string Name { get; set; }
    }
   
}
