using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.core.Interfaces
{
    public interface IUnitOfWork
    {
        Task<IGenericRepo<T>> repository<T>() where T : class;
        Task CompleteAsync();
    }
}
