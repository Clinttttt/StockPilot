using MediatR;
using StockPilot.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockPilot.Application.Features.Command.Supplier.CreateSupplier
{
    public record CreateSupplierCommand(string FullName,
           string PhoneNumber,
           string Email,
           string Address) : IRequest<Result<Guid>>;
}
