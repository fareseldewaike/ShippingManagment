using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shipping.core.Models;

namespace Shipping.core.Interfaces
{
    public interface IProductRepo
    {
        Task<List<Product>> GetByIdAsync(int id);
        Task<bool> AddRange(List<Product> products);
        Task<bool> DeleteAsync(List<Product> products);

    }
}

