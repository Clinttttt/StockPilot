using System;
using System.Collections.Generic;
using System.Text;

namespace StockPilot.Application.Common.Interfaces.Services
{
    public interface ICurrentUserService
    {
        string UserId { get; }
        string Name { get; }
        string Role { get; }
    }
}
