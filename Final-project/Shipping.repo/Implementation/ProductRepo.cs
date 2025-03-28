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
        public class ProductRepo : IProductRepo<T> where T : class
        {
            private readonly ShippingContext _context;

            public ProductRepo(ShippingContext context)
            {
                _context = context;
            }

            public async Task GetByIdAsync(int id)
            {
                return await _context.Products.FindAsync(id);
            }

            public async Task AddAsync(Product product)
            {
                await _context.Products.AddAsync(product);
                await _context.SaveChangesAsync();
            }

            public async Task DeleteAsync(int id)
            {
                var product = await GetByIdAsync(id);
                if (product != null)
                {
                    _context.Products.Remove(product);
                    await _context.SaveChangesAsync();
                }
            }
        }
    }
}
}
