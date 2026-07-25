using MediatR;
using StockPilot.Application.Common.Model;
using StockPilot.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockPilot.Application.Features.Queries.Product.ListingProduct
{
    public record ListingProductQuery(int PageSize, int pageNumber, Guid? CategoryId, string? search, bool lowStock = false, bool IsActive = true) : IRequest<Result<PaginatedList<ListingProductQueryDto>>>;
    
}
