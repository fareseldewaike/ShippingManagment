using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTOs.DTO.Order;
using Shipping.core.Models;

namespace Shipping.core.Interfaces
{
    public interface IOrderRepo
    {
        Task<int> CreateOrder(Order order);
        Task<bool> UpdateOrder(Order order);
        Task<bool> DeleteOrder(int id);
        Task<Order> GetOrderById(int id);
        Task<List<Order>> GetAllOrders();
        Task<List<Order>> GetOrdersByMerchantId(int MerchantId);
        Task<List<Order>> GetOrdersByDateRangeAndStatus(DateTime startDate, DateTime endDate, string status);
        Task<List<Order>> GetOrdersByBranchId(int branchId);
        Task<List<Order>> GetOrdersByRepresentativeId(string representativeId);
        

    }
}
