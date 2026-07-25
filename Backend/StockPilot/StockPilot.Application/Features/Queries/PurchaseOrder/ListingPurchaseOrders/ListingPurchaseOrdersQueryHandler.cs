using MediatR;
using Microsoft.EntityFrameworkCore;
using StockPilot.Application.Common.Extensions;
using StockPilot.Application.Common.Interfaces.Data;
using StockPilot.Application.Common.Model;
using StockPilot.Domain.Common;
using StockPilot.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace StockPilot.Application.Features.Queries.PurchaseOrder.ListingPurchaseOrders
{
    public class ListingPurchaseOrdersQueryHandler(IAppDbContext context) : IRequestHandler<ListingPurchaseOrdersQuery, Result<PaginatedList<PurchaseOrderListItemDto>>>
    {
        public async Task<Result<PaginatedList<PurchaseOrderListItemDto>>> Handle(ListingPurchaseOrdersQuery request, CancellationToken cancellationToken)
        {

            var suppliers = await context.suppliers.FirstOrDefaultAsync(s => s.Id == request.SupplierId,cancellationToken);

            if(suppliers is null)         
                return Result<PaginatedList<PurchaseOrderListItemDto>>.NotFound("Suppliers not found");

            var query = context.purchaseOrders
                .AsNoTracking()
                .Where(s => s.SupplierId == request.SupplierId);

            var search = request.Search?.Trim();
            var fromDate = request.FromDate?.Date;
            var toDateExclusive = request.ToDate?.Date.AddDays(1);

            query = query
                .WhereIf(
                    !string.IsNullOrWhiteSpace(search),
                    order => order.PoNumber.Contains(search!))
                .WhereIf(
                    fromDate.HasValue,
                    order => order.OrderDate >= fromDate)
                .WhereIf(
                    toDateExclusive.HasValue,
                    order => order.OrderDate < toDateExclusive)
                .WhereIf(
                    request.Status.HasValue,
                    order => order.orderStatus == request.Status);

            var PurchaseOrders = query
             .Select(s => new PurchaseOrderListItemDto(
             PurchaseOrderId: s.Id,
             PoNumber: s.PoNumber,
             SupplierName: suppliers.FullName,
             OrderDate: s.OrderDate,
             ExpectedDeliveryDate: s.ExpectedDeliveryDate,
             Status: s.orderStatus,
             ItemCount: s.Items.Count,
             TotalAmount: s.Items.Sum(s => s.QuantityOrdered * s.UnitCost)
         ));

            var paginated = await QueryableExtensions.PaginatedAsync(PurchaseOrders, request.PageSize, request.PageNumber);
            return Result<PaginatedList<PurchaseOrderListItemDto>>.Success(paginated);
        }
    }
}
