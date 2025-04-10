using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.DTO
{

    public enum AmountType
    {
        Percent,
        Fixed
    }

    public class representativeadd
    {





        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public int BranchId { get; set; }

        public double? Amount { get; set; }
        public AmountType? Type { get; set; }



        public string PhoneNumber { get; set; } = string.Empty;
        public string Password { get; set; }
    }
}