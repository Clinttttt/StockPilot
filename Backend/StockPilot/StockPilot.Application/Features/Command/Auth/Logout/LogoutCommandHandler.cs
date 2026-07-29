using MediatR;
using StockPilot.Application.Common.Interfaces.Services;
using StockPilot.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockPilot.Application.Features.Command.Auth.Logout
{
    internal class LogoutCommandHandler(ITokenServices tokenServices) : IRequestHandler<LogoutCommand, Result>
    {
        public async Task<Result> Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            await tokenServices.RevokeRefreshtoken(request.refreshToken, cancellationToken);
            return Result.Success();
        }
    }
}
