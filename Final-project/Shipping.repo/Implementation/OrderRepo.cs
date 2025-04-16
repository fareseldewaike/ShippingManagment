using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTOs.DTO.Order;
using Microsoft.EntityFrameworkCore;
using Shipping.core.Interfaces;
using Shipping.core.Models;
using Shipping.repo.ShippingCon;


namespace Shipping.repo.Implementation
{
    public class OrderRepo : IOrderRepo
    {
        private readonly ShippingContext _context;
        public OrderRepo(ShippingContext context)
        {
            _context = context;
        }
        public async Task<int> CreateOrder(Order order)
        {

           await _context.Orders.AddAsync(order);
            return await _context.SaveChangesAsync();

        }

        public Task<bool> DeleteOrder(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Order>> GetAllOrders()
        {
            var orders = await _context.Orders
       .Include(o => o.Governorate)
       .Include(o => o.Merchant)
       .ToListAsync();
            return orders;

        }

        public async Task<Order> GetOrderById(int id)
        {
            var order = await _context.Orders
          .Include(o => o.Governorate)
          .Include(o => o.Merchant)
          .FirstOrDefaultAsync(o => o.Id == id && o.isDeleted == false);

            return order;

        }

     

        public Task<List<Order>> GetOrdersByBranchId(int branchId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Order>> GetOrdersByDateRangeAndStatus(DateTime startDate, DateTime endDate, string status)
        {
            throw new NotImplementedException();

        }

        public Task<List<Order>> GetOrdersByMerchantId(int MerchantId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Order>> GetOrdersByRepresentativeId(string representativeId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateOrder(Order order)
        {

            return await _context.SaveChangesAsync() > 0;
        }

        
    }
}
