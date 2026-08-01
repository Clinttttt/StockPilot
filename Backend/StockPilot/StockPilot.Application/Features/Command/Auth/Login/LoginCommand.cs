using MediatR;
using StockPilot.Application.Dtos;
using StockPilot.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;
namespace StockPilot.Application.Features.Command.Auth.Login
{
    public record LoginCommand(string? UserName, string? Password) : IRequest<Result<TokenResponseDto>>;
    
}
