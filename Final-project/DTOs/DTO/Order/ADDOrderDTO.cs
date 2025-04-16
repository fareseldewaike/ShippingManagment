using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;





namespace DTOs.DTO.Order
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
    public class ADDOrderDTO
    {

        public string ClientName { get; set; } = string.Empty;
        public string FirstPhoneNumber { get; set; } = string.Empty;
        public string? SecondPhoneNumber { get; set; }
        public string Email { get; set; } = string.Empty;
        public PaymentType PaymentType { get; set; }
        public OrderType orderType { get; set; }
        public OrderStatus orderStatus { get; set; }
        public int CityId { get; set; }
        public int GovernorateId { get; set; }
        public ShippingType ShippingType { get; set; }

        public int BranchId { get; set; }

        public Boolean? DeliverToVillage { get; set; } 

        public List<ProductDTO> Products { get; set; } 
        public double OrderTotalWeight { get; set; }
        public double OrderCost { get; set; }

        public string MerchantId { get; set; }

        public string Notes { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;

    }
}
