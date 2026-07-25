using MediatR;
using StockPilot.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockPilot.Application.Features.Command.Supplier.UpdateSupplier
{
    public record UpdateSupplierCommand(Guid SupplierId, string FullName,
           string PhoneNumber,
           string Email,
           string Address) : IRequest<Result<bool>>;
  
}
