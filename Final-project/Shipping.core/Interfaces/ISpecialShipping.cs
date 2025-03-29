using Shipping.core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.core.Interfaces
{
    public interface ISpecialPrice
    {
        Task<List<SpecialPrice>> GetSpecialPricesByMerchantId(string Id);
        Task<int> AddRangeAsync(List<SpecialPrice> specialPrices);
        Task<int> RemoveRangeAsync(List<SpecialPrice> specialPrices);
    }
}
