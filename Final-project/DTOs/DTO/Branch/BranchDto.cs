using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.DTO.Branch
{
    public class BranchDto
    {
        public int Id { get; set; }
        public string Name { get; set; } 
        public bool status { get; set; }
        public DateTime DateTime { get; set; }
    }
}
