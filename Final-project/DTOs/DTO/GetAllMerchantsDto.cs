using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.DTO
{
    public class GetAllMerchantsDto
    {
        public string Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public double? ReturnerPercent { get; set; }
        public string? BranchName { get; set; }
        public string? StoreName { get; set; }
        public string? GovernateName { get; set; }
        public bool IsDeleted { get; set; }
    }
}
