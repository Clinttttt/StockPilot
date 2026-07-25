using MediatR;
using StockPilot.Application.Common.Model;
using StockPilot.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockPilot.Application.Features.Queries.Supplier.GetSupplier
{
    public record GetSupplierQuery
        (
        string search, 
        bool isActive,
        int pageNumber,
        int pageSize
        ) : IRequest<Result<PaginatedList<SupplierListItemDto>>>; 
   
}
