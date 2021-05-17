using Microsoft.AspNetCore.Authorization;
using MoneyTruckTrackingAPI.Helpers;
using MoneyTruckTrackingAPI.Requirements;
using System;
using System.Threading.Tasks;

namespace MoneyTruckTrackingAPI.Handlers
{
    public class AccountAdminHandler : AuthorizationHandler<AccountAdminRequirement>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context, 
            AccountAdminRequirement requirement)
        {
            if(!context.User.HasClaim(c => c.Type == "IsAdmin" && c.Issuer == TokenHelper.ISSUER))
            {
                return Task.CompletedTask;
            }

            string value = context.User.FindFirst(c => c.Type == "IsAdmin" && c.Issuer == TokenHelper.ISSUER).Value;
            var isAdmin = Convert.ToBoolean(value);

            if(isAdmin == requirement.IsAdmin)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
