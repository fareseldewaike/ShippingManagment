using Shipping.core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.core.Interfaces
{
    public interface IGroupPermissionRepo
    {
        Task<List<GroupPermission>> GetGroupPermissionsByGroupId(int Id);
        Task<int> AddRangeAsync(List<GroupPermission> groupPermissions);
        Task<int> RemoveRangeAsync(List<GroupPermission> groupPermissions);
    }
}
