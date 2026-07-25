using MediatR;
using StockPilot.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;
using static StockPilot.Domain.Entities.Enums;

namespace StockPilot.Application.Features.Command.Auth.Register
{
    public record RegisterCommand
        (string FullName, string ContactPerson,
        string PhoneNumber, string Email,string Address,
        string UserName, string PasswordHash, UserRole role) 
        : IRequest<Result<bool>>;
}
