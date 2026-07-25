using MediatR;
using StockPilot.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockPilot.Application.Features.Command.Supplier.SetSupplierStatus
{
    public record SetSupplierStatusCommand(Guid SupplierId, bool IsActive) : IRequest<Result>;
  
}
