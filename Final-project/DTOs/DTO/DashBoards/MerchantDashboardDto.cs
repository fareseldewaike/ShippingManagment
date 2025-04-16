using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.DTO.DashBoards
{
    public class MerchantDashboardDto
    {
        public int TotalOrders { get; set; }
        public int New { get; set; }
        public int Pending { get; set; }
        public int RepresentitiveDelivered { get; set; }
        public int ClientDelivered { get; set; }
        public int UnReachable { get; set; }
        public int Postponed { get; set; }
        public int PartiallyDelivered { get; set; }
        public int ClientCanceled { get; set; }
        public int RejectWithPaying { get; set; }
        public int RejectWithPartialPaying { get; set; }
        public int RejectFromEmployee { get; set; }
    }
}
