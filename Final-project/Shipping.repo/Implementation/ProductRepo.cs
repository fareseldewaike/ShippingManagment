using System;
using Microsoft.EntityFrameworkCore;
using Shipping.core.Interfaces;
using Shipping.repo.ShippingCon;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shipping.core.Models;

namespace Shipping.repo.Implementation
{
    public class ProductRepo : IProductRepo
    {
        private readonly ShippingContext _context;

        public ProductRepo(ShippingContext context)
        {
            _context = context;
        }

        public async Task<bool> AddRange(List<Product> products)
        {
            await _context.Products.AddRangeAsync(products);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(List<Product> products)
        {
           _context.Products.RemoveRange(products);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Product>> GetByIdAsync(int id)
        {

            var products = await _context.Products.Where(p => p.OrderId == id).ToListAsync();
            return  products;

        }
    }
}
