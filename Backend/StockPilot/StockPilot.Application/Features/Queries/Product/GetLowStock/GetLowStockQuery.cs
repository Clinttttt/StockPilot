using MediatR;
using StockPilot.Application.Common.Model;
using StockPilot.Application.Dtos;
using StockPilot.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockPilot.Application.Features.Queries.Product.GetLowStock
{
    public sealed record GetLowStockQuery(
    string? Search,
    Guid? CategoryId,
    int PageNumber = 1,
    int PageSize = 20
    ) : IRequest<Result<PaginatedList<LowStockProductDto>>>;
}
