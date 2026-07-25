using Microsoft.AspNetCore.Http;
using StockPilot.Application.Common;
using StockPilot.Application.Common.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;
using System.Security.Claims;
using System.Text;

namespace StockPilot.Infrastructure.Services
{
    public class CurrentUserService : ICurrentUserService
    {
       private readonly IHttpContextAccessor _httpContext;
        public CurrentUserService(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        } 

        public string UserId => _httpContext.HttpContext.User.FindFirstValue(AppClaimTypes.UserId) ?? string.Empty;
        public string Name => _httpContext.HttpContext.User.FindFirstValue(AppClaimTypes.UserName) ?? string.Empty;
        public string Role => _httpContext.HttpContext.User.FindFirst(AppClaimTypes.UserRole)!.ToString() ?? string.Empty;
      
    }
}
