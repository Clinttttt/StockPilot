using MediatR;
using StockPilot.Application.Common.Model;
using StockPilot.Domain.Common;
using StockPilot.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockPilot.Application.Features.Queries.PurchaseOrder.ListingPurchaseOrders
{
    public sealed record ListingPurchaseOrdersQuery(
        string? Search,
        Guid SupplierId,
        PurchaseOrderStatus? Status,
        DateTime? FromDate,
        DateTime? ToDate,
        int PageNumber = 1,
        int PageSize = 20

        )
    : IRequest<Result<PaginatedList<PurchaseOrderListItemDto>>>;
    
}
