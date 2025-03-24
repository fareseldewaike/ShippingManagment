using Microsoft.EntityFrameworkCore;
using Shipping.core.Interfaces;
using Shipping.repo.ShippingCon;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.repo.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {

        public Hashtable _repositories;
        private readonly ShippingContext _shippingContext;

        public UnitOfWork(ShippingContext shippingContext)
        {
            _shippingContext = shippingContext;
            _repositories = new Hashtable();
        }

        public   IGenericRepo<T> Repository<T>() where T : class
        {
             var name = typeof(T).Name;
            if (!_repositories.ContainsKey(name))
            {
               var repo = new GenericRepo<T>(_shippingContext);
                _repositories.Add(name, repo);
                 
            }
            return (IGenericRepo<T>)_repositories[name];
        }

        public async Task<int> CompleteAsync()
        {
            return await _shippingContext.SaveChangesAsync();
        }
        public async ValueTask DisposeAsync()
        {
            await _shippingContext.DisposeAsync();
        }
    }
}
