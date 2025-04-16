using DTOs.DTO.Order;
using Shipping.core.Interfaces;
using Shipping.core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShippingType = DTOs.DTO.Order.ShippingType;

namespace Shipping.services
{
    public class OrderService
    {
        private readonly IOrderRepo _orderRepo;
        private readonly IProductRepo _productRepo;
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IOrderRepo orderRepo, IProductRepo productRepo , IUnitOfWork unitOfWork)
        {
            _orderRepo = orderRepo;
            _productRepo = productRepo;
            _unitOfWork = unitOfWork;
        }

        public async Task CreateOrder(ADDOrderDTO order)
        {

            var newOrder = new Order
            {
                ClientName = order.ClientName,
                FirstPhoneNumber = order.FirstPhoneNumber,
                SecondPhoneNumber = order.SecondPhoneNumber,
                Email = order.Email,
                GovernorateId = order.GovernorateId,
                CityId = order.CityId,
                Date = DateTime.Now,
                MerchantId = order.MerchantId,
                BranchId = order.BranchId,
                DeliverToVillage = order.DeliverToVillage,
                ShippingTypeId = (int)order.ShippingType,
                orderStatus = (core.Models.OrderStatus)order.orderStatus,
                orderType = (core.Models.OrderType)order.orderType,
                PaymentType = (core.Models.PaymentType)order.PaymentType,
                ProductTotalCost = order.OrderCost,
                OrderShippingTotalCost = order.OrderCost,
                Weight = order.OrderTotalWeight,
                Street = order.Street,
                Notes = order.Notes,
                isDeleted = false,
                ReasonsRefusalTypeId = null,
                Products = order.Products.Select(p => new Product
                {
                    Name = p.Name,
                    Quantity = p.Quantity,
                    Weight = p.Weight,
                   // OrderId = 0 // This will be set when the order is saved
                }).ToList()
            };
             await _orderRepo.CreateOrder(newOrder);
            // await _productRepo.AddRange(newOrder.Products.ToList());
        }
        public async Task<Order> UpdateOrder(int id, ADDOrderDTO order)
        {
            var existingOrder = await _orderRepo.GetOrderById(id);
            if (existingOrder == null)
            {
                throw new Exception("Order not found");
            }
            existingOrder.ClientName = order.ClientName;
            existingOrder.FirstPhoneNumber = order.FirstPhoneNumber;
            existingOrder.SecondPhoneNumber = order.SecondPhoneNumber;
            existingOrder.Email = order.Email;
            existingOrder.GovernorateId = order.GovernorateId;
            existingOrder.CityId = order.CityId;
            existingOrder.Date = DateTime.Now;
            existingOrder.MerchantId = order.MerchantId;
            existingOrder.BranchId = order.BranchId;
            existingOrder.DeliverToVillage = order.DeliverToVillage;
            existingOrder.ShippingTypeId = (int)order.ShippingType;
            existingOrder.orderStatus = (core.Models.OrderStatus)order.orderStatus;
            existingOrder.orderType = (core.Models.OrderType)order.orderType;
            existingOrder.PaymentType = (core.Models.PaymentType)order.PaymentType;
            existingOrder.ProductTotalCost = order.OrderCost;
            existingOrder.OrderShippingTotalCost = order.OrderCost;
            existingOrder.Weight = order.OrderTotalWeight;
            existingOrder.Street = order.Street;
            existingOrder.Notes = order.Notes;
            existingOrder.isDeleted = false;
            existingOrder.Products = order.Products.Select(p => new Product
            {
                Name = p.Name,
                Quantity = p.Quantity,
                Weight = p.Weight,
                // OrderId = existingOrder.Id // This will be set when the order is saved
            }).ToList();

            // await _productRepo.UpdateRange(existingOrder.Products.ToList());
            await _orderRepo.UpdateOrder(existingOrder);
            return existingOrder;
        }
    
        public async Task<bool> DeleteOrder(int id)
        {
            var order = await _orderRepo.GetOrderById(id);
            if (order == null)
            {
                throw new Exception("Order not found");
            }
            order.isDeleted = true;
            await _orderRepo.UpdateOrder(order);
            return true;
        }
        public async Task<List<ReportDTO>> GetOrderReport(int pageSize, int pageNum ,DateTime? fromDate , DateTime? toDate , DTOs.DTO.Order.OrderStatus? status )
        {
            var orderss = await _orderRepo.GetAllOrders();
            var orders =orderss.Where(orderss => orderss.isDeleted == false).Skip((pageNum - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            var baseweight =await _unitOfWork.Repository<Weight>().GetAllAsync();
            var lastupdatedWeight = baseweight.FirstOrDefault();
            if (orders == null || !orders.Any())
            {
                throw new Exception("No orders found.");
            }
            if (fromDate.HasValue)
                orders = orders.Where(o => o.Date >= fromDate.Value).ToList();

            if (toDate.HasValue)
                orders = orders.Where(o => o.Date <= toDate.Value).ToList();

            if (status.HasValue)
                orders = orders.Where(o => (int)o.orderStatus == (int)status.Value).ToList();
            var reportDTOs = orders.Select(o =>
            {
                double baseCost = o.City.Price; // instead of 50

                double? specialPrice = (double?)(
                    o.Merchant?.SpecialPrices?
                        .FirstOrDefault(sp => sp.City?.Name == o.City?.Name)?.Price
                );

                double shippingCost = baseCost + (specialPrice ?? 0.0);

                if (o.Weight > lastupdatedWeight.DefaultWeight)
                {
                    double extraWeightCost = (o.Weight - lastupdatedWeight.DefaultWeight) * lastupdatedWeight.AdditionalPrice;
                    shippingCost += extraWeightCost;
                }

                if (o.DeliverToVillage == true)
                {
                    shippingCost += 40.0;
                }
                if ((int)o.ShippingType == (int)ShippingType.ShippingIn24Hours)
                {
                    shippingCost += 50.0;
                }
                 if ((int)o.ShippingTypeId == (int)ShippingType.ShippingIn15Days)
                {
                    shippingCost += 20.0;
                }

                // Safeguard in case Representative is null
                double companyAmount = 0.0;
                if (o.Representative != null)
                {
                    companyAmount = (double)(o.Representative.Type == 0
                        ? (o.Representative.Amount / 100.0) * shippingCost
                        : o.Representative.Amount);
                }

                return new ReportDTO
                {
                    SerialNumber = o.Id,
                    ClientName = o.ClientName,
                    PhoneNumber = o.FirstPhoneNumber,
                    Governorate = o.Governorate?.Name ?? "Unknown",
                    City = o.City?.Name ?? "Unknown",
                    Date = o.Date,
                    MerchantName = o.Merchant?.Name ?? "Unknown",
                    OrderCost = o.ProductTotalCost,
                    OrderStatus = (DTOs.DTO.Order.OrderStatus)o.orderStatus,
                    paidAmount = 0,
                    shippingCost = shippingCost,
                    paidshippingamount = shippingCost,
                    companyAmount = companyAmount
                };
            }).ToList();

            return reportDTOs;
        }
        public async Task<List<Order>> GetOrdersByBranchId(int branchId)
        {
            var orders = await _orderRepo.GetOrdersByBranchId(branchId);
            if (orders == null || !orders.Any())
            {
                throw new Exception("No orders found.");
            }
            return orders;
        }

        public async Task<List<Order>> GetOrdersByDateRangeAndStatus(int pageSize, int pageNum,DateTime startDate, DateTime endDate, string status)
        {
            var orderss = await _orderRepo.GetAllOrders();
            var orders = orderss.Where(orderss => orderss.isDeleted == false).Skip((pageNum - 1) * pageSize)
              .Take(pageSize)
              .ToList();
            if (orders == null || !orders.Any())
            {
                throw new Exception("No orders found.");
            }
            return orders;
        }



    }
}
