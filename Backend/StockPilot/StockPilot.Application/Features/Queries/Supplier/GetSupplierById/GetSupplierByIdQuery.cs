using MediatR;
using StockPilot.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockPilot.Application.Features.Queries.Supplier.GetSupplierById
{
    public record GetSupplierByIdQuery(Guid SupplierId) : IRequest<Result<GetSupplierDto>>;
  
}
