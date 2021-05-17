using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyTruckTrackingAPI.Requirements
{
    public class AccountAdminRequirement : IAuthorizationRequirement
    {
        public bool IsAdmin { get; }

        public AccountAdminRequirement(bool isAdmin)
        {
            IsAdmin = isAdmin;
        }
    }
}
