using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.core.Models
{
    public enum OrderStatus
    {
        New,
        Pending,
        RepresentitiveDelivered,
        ClientDelivered,
        UnReachable,
        Postponed,
        PartiallyDelivered,
        ClientCanceled,
        RejectWithPaying,
        RejectWithPartialPaying,
        RejectFromEmployee,
    }
    public enum PaymentType
    {
        payOnDelivery,
        prepaid,
        packageForAPackage

    }

    public enum OrderType
    {
        ReceiveFromTheBranch,
        ReceiveFromTheTrader
    }
    public enum ShippingType
    {
        NormalShipping,
        ShippingIn24Hours,
        ShippingIn15Days
    }

    public class Order
    {
        public int Id { get; set; }
        public string ClientName { get; set; } = string.Empty;
        public string FirstPhoneNumber { get; set; } = string.Empty;
        public string? SecondPhoneNumber { get; set; }
        public string Email { get; set; } = string.Empty;
        public PaymentType PaymentType { get; set; }
        public OrderType orderType { get; set; }
        public OrderStatus orderStatus { get; set; }

        [ForeignKey("Governorate")]
        public int GovernorateId { get; set; }

        
        public string Street { get; set; } = string.Empty;

        public bool? DeliverToVillage { get; set; }

        [ForeignKey("ShippingType")]
        public int ShippingTypeId { get; set; }

        [ForeignKey("Branch")]
        public int BranchId { get; set; }


        public DateTime Date { get; set; }

        public string? Notes { get; set; }

        public bool isDeleted { get; set; } = false;

        public double ProductTotalCost { get; set; }

        public double OrderShippingTotalCost { get; set; }

        public double Weight { get; set; }

        [ForeignKey("ReasonsRefusalType")]
        public int? ReasonsRefusalTypeId { get; set; }

        [ForeignKey("Merchant")]
        public string MerchantId { get; set; } = string.Empty;

        [ForeignKey("Representative")]
        public string? RepresentativeId { get; set; }

        public virtual Rejection? Rejection { get; set; }
        public virtual Governorate? Governorate { get; set; }
        
       public ShippingType ShippingType { get; set; }=ShippingType.NormalShipping;

        public virtual Branch? Branch { get; set; }
        public virtual Representative? Representative { get; set; }
        public virtual Merchant? Merchant { get; set; }
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    }
}
