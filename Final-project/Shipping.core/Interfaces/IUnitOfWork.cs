using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.core.Interfaces
{
    public interface IUnitOfWork : IAsyncDisposable
    {
       public IGenericRepo<T> Repository<T>() where T : class;
        Task<int> CompleteAsync();
    }
}
