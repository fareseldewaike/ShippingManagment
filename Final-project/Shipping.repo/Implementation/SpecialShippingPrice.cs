using Shipping.core.Interfaces;
using Shipping.core.Models;
using Shipping.repo.ShippingCon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.repo.Implementation
{
    public class SpecialShippingPrice : ISpecialPrice
    {
        private readonly ShippingContext _Context;

        public SpecialShippingPrice(ShippingContext shippingContext)
        {
            _Context = shippingContext;
        }
      public async  Task<int> AddRangeAsync(List<core.Models.SpecialPrice> specialPrices)
        {
            if (specialPrices == null || specialPrices.Count == 0) {
                return 0;
            }   
            _Context.SpecialPrices.AddRange(specialPrices);
            return _Context.SaveChanges();
        }

       public async Task<List<SpecialPrice>>  GetSpecialPricesByMerchantId(string Id)
        {
            if(string.IsNullOrEmpty(Id))
            {
                return null;
            }
            var prices=   _Context.SpecialPrices.Where(x => x.MerchentId == Id).ToList();
 
            return prices;
        }

        public async Task<int>  RemoveRangeAsync(List<core.Models.SpecialPrice> specialPrices)
        {
            if(specialPrices == null || specialPrices.Count == 0)
            {
                return 0;
            }
            _Context.SpecialPrices.RemoveRange(specialPrices);
            return _Context.SaveChanges();
        }
    }
}
