using Microsoft.EntityFrameworkCore;
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
    public class GroupPermissionRepo : IGroupPermissionRepo
    {
        private readonly ShippingContext _shippingContext;

        public GroupPermissionRepo(ShippingContext shippingContext)
        {
            _shippingContext = shippingContext;
        }

        public async Task<int> AddRangeAsync(List<GroupPermission> groupPermissions)
        {
            await _shippingContext.GroupPermissions.AddRangeAsync(groupPermissions);
            return await _shippingContext.SaveChangesAsync();
        }

        public async Task<List<GroupPermission>> GetGroupPermissionsByGroupId(int groupId)
        {
            return await _shippingContext.GroupPermissions
                .Where(gp => gp.GroupId == groupId)
                .ToListAsync();
        }

        public async Task<int> RemoveRangeAsync(List<GroupPermission> groupPermissions)
        {
            _shippingContext.GroupPermissions.RemoveRange(groupPermissions);
            return await _shippingContext.SaveChangesAsync();
        }
    }
}
