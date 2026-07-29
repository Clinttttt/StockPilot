using MediatR;
using StockPilot.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockPilot.Application.Features.Command.Auth.Logout
{
    public sealed record LogoutCommand(string refreshToken) : IRequest<Result>;
 
}
