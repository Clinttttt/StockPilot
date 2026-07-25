using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace StockPilot.Application.Common
{
    public static class AppClaimTypes
    {   
        public const string UserId = ClaimTypes.NameIdentifier;
        public const string UserName = ClaimTypes.Name;
        public const string UserEmail = ClaimTypes.Email;
        public const string UserRole = ClaimTypes.Role;

    }
}
