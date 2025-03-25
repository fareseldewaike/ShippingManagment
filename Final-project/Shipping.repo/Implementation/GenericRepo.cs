using Microsoft.EntityFrameworkCore;
using Shipping.core.Interfaces;
using Shipping.repo.ShippingCon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.repo.Implementation
{
    public class GenericRepo<T> : IGenericRepo<T> where T : class
    {
        private readonly ShippingContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepo(ShippingContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
      
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
          
        }

        public async Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
   
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
