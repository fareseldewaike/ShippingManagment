using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Shipping.core.Models;
using Shipping.repo.ShippingCon;
using System.Security.Claims;

namespace Shipping.CustomAttribtues
{
    public class HasPermissionAttribute : Attribute, IAsyncAuthorizationFilter
    {
        private readonly string _permission;
        private readonly string _action;

        public HasPermissionAttribute(string permission, string action)
        {
            _permission = permission;
            _action = action;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context) // automatic execute
        {
            var userId = context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                context.Result = new ForbidResult();
                return;
            }

            var db = context.HttpContext.RequestServices.GetRequiredService<ShippingContext>();

            var user = await db.Users.Cast<Employee>()
                .Include(u => u.Group)
                    .ThenInclude(g => g.GroupPermissions)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null || user.Group == null)
            {
                context.Result = new ForbidResult();
                return;
            }

            var hasPermission = user.Group.GroupPermissions
                .Any(gp => gp.Permission.Name == _permission && gp.Action == _action);

            if (!hasPermission)
            {
                context.Result = new ForbidResult();
            }
        }
    }
}
