using Shipping.core.Interfaces;
using Shipping.core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.services
{
    public class GroupService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGroupPermissionRepo _groupPermissionRepo;

        public GroupService(IUnitOfWork unitOfWork , IGroupPermissionRepo groupPermissionRepo)

        {
            _unitOfWork = unitOfWork;
            _groupPermissionRepo = groupPermissionRepo;
        }
        public async Task<List<GroupPermission>> GetGroupPermissionsAsync(int groupId)
        {
            return await _groupPermissionRepo.GetGroupPermissionsByGroupId(groupId);
        }

        public async Task<bool> AddGroupPermissionsAsync(List<GroupPermission> groupPermissions)
        {
            var result = await _groupPermissionRepo.AddRangeAsync(groupPermissions);
            return result > 0;
        }

        public async Task<bool> RemoveGroupPermissionsAsync(int groupId)
        {
            var permissions = await _groupPermissionRepo.GetGroupPermissionsByGroupId(groupId);
            if (!permissions.Any()) return false;

            var result = await _groupPermissionRepo.RemoveRangeAsync(permissions);
            return result > 0;
        }
    }
}
