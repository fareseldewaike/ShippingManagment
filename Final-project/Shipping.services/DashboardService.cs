using Shipping.core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTOs.DTO.DashBoards;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Shipping.repo.ShippingCon;




namespace Shipping.services
{
    public class DashboardService

    {
        private readonly ShippingContext _context;

        public DashboardService(ShippingContext context)
        {
            _context = context;
        }

        private async Task<Dictionary<OrderStatus, int>> GetOrderStatusCountsAsync(Expression<Func<Order, bool>> predicate) // o => o.MerchantId == merchantI
        {
            return await _context.Orders
                .Where(predicate)
                .GroupBy(o => o.orderStatus)  // new =>{order1,order2} ,
                .Select(g => new { Status = g.Key, Count = g.Count() })
                .ToDictionaryAsync(x => x.Status, x => x.Count);
        }

        private async Task<int> GetTotalOrdersAsync(Expression<Func<Order, bool>> predicate)
        {
            return await _context.Orders.CountAsync(predicate);
        }

        public async Task<MerchantDashboardDto> GetMerchantDashboardAsync(string merchantId)
        {
            var predicate = (Expression<Func<Order, bool>>)(o => o.MerchantId == merchantId);
            var total = await GetTotalOrdersAsync(predicate);
            var statusCounts = await GetOrderStatusCountsAsync(predicate);

            return new MerchantDashboardDto
            {
                TotalOrders = total,
                New = statusCounts.GetValueOrDefault(OrderStatus.New),
                Pending = statusCounts.GetValueOrDefault(OrderStatus.Pending),
                RepresentitiveDelivered = statusCounts.GetValueOrDefault(OrderStatus.RepresentitiveDelivered),
                ClientDelivered = statusCounts.GetValueOrDefault(OrderStatus.ClientDelivered),
                UnReachable = statusCounts.GetValueOrDefault(OrderStatus.UnReachable),
                Postponed = statusCounts.GetValueOrDefault(OrderStatus.Postponed),
                PartiallyDelivered = statusCounts.GetValueOrDefault(OrderStatus.PartiallyDelivered),
                ClientCanceled = statusCounts.GetValueOrDefault(OrderStatus.ClientCanceled),
                RejectWithPaying = statusCounts.GetValueOrDefault(OrderStatus.RejectWithPaying),
                RejectWithPartialPaying = statusCounts.GetValueOrDefault(OrderStatus.RejectWithPartialPaying),
                RejectFromEmployee = statusCounts.GetValueOrDefault(OrderStatus.RejectFromEmployee)
            };
        }

        public async Task<EmployeeDashboardDto> GetEmployeeDashboardAsync()
        {
            var predicate = (Expression<Func<Order, bool>>)(o => true); // كل الأوردرات
            var total = await GetTotalOrdersAsync(predicate);
            var statusCounts = await GetOrderStatusCountsAsync(predicate);

            return new EmployeeDashboardDto
            {
                TotalOrders = total,
                New = statusCounts.GetValueOrDefault(OrderStatus.New),
                Pending = statusCounts.GetValueOrDefault(OrderStatus.Pending),
                RepresentitiveDelivered = statusCounts.GetValueOrDefault(OrderStatus.RepresentitiveDelivered),
                ClientDelivered = statusCounts.GetValueOrDefault(OrderStatus.ClientDelivered),
                UnReachable = statusCounts.GetValueOrDefault(OrderStatus.UnReachable),
                Postponed = statusCounts.GetValueOrDefault(OrderStatus.Postponed),
                PartiallyDelivered = statusCounts.GetValueOrDefault(OrderStatus.PartiallyDelivered),
                ClientCanceled = statusCounts.GetValueOrDefault(OrderStatus.ClientCanceled),
                RejectWithPaying = statusCounts.GetValueOrDefault(OrderStatus.RejectWithPaying),
                RejectWithPartialPaying = statusCounts.GetValueOrDefault(OrderStatus.RejectWithPartialPaying),
                RejectFromEmployee = statusCounts.GetValueOrDefault(OrderStatus.RejectFromEmployee)
            };
        }

        public async Task<RepresentativeDashboardDto> GetRepresentativeDashboardAsync(string representativeId)
        {
            var predicate = (Expression<Func<Order, bool>>)(o => o.RepresentativeId == representativeId);
            var total = await GetTotalOrdersAsync(predicate);
            var statusCounts = await GetOrderStatusCountsAsync(predicate);

            return new RepresentativeDashboardDto
            {
                TotalOrders = total,
                New = statusCounts.GetValueOrDefault(OrderStatus.New),
                Pending = statusCounts.GetValueOrDefault(OrderStatus.Pending),
                RepresentitiveDelivered = statusCounts.GetValueOrDefault(OrderStatus.RepresentitiveDelivered),
                ClientDelivered = statusCounts.GetValueOrDefault(OrderStatus.ClientDelivered),
                UnReachable = statusCounts.GetValueOrDefault(OrderStatus.UnReachable),
                Postponed = statusCounts.GetValueOrDefault(OrderStatus.Postponed),
                PartiallyDelivered = statusCounts.GetValueOrDefault(OrderStatus.PartiallyDelivered),
                ClientCanceled = statusCounts.GetValueOrDefault(OrderStatus.ClientCanceled),
                RejectWithPaying = statusCounts.GetValueOrDefault(OrderStatus.RejectWithPaying),
                RejectWithPartialPaying = statusCounts.GetValueOrDefault(OrderStatus.RejectWithPartialPaying),
                RejectFromEmployee = statusCounts.GetValueOrDefault(OrderStatus.RejectFromEmployee)
            };
        }


    }
}
