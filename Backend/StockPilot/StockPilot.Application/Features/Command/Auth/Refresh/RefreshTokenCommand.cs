using MediatR;
using StockPilot.Application.Dtos;
using StockPilot.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockPilot.Application.Features.Command.Auth.Refresh
{
    public sealed record RefreshTokenCommand(string RefreshToken) : IRequest<Result<TokenResponseDto>>;
   
}
