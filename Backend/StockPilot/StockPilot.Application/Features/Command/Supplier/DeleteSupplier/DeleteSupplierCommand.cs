using MediatR;
using StockPilot.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockPilot.Application.Features.Command.Supplier.DeleteSupplier
{
    public record DeleteSupplierCommand(Guid SupplierId) : IRequest<Result>;

}
