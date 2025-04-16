using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.DTO.Employee
{
    public class EmployeeDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public int BranchId { get; set; }
        public string BranchName { get; set; }
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public bool IsDeleted { get; set; }
    }
}
