using MediatR;
using StockPilot.Application.Common.Model;
using StockPilot.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockPilot.Application.Features.Queries.Product.GetLowStock
{
    internal class GetLowStockQueryHandler() : IRequestHandler<GetLowStockQuery, Result<PaginatedList<LowStockProductDto>>>
    {
        public Task<Result<PaginatedList<LowStockProductDto>>> Handle(GetLowStockQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
