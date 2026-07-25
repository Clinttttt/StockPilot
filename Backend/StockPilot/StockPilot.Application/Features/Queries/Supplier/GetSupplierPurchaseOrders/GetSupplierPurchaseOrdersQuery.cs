using MediatR;
using StockPilot.Domain.Common;
using StockPilot.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockPilot.Application.Features.Queries.Supplier.GetSupplierPurchaseOrders
{
    public sealed record GetSupplierPurchaseOrdersQuery(Guid SupplierId,
    PurchaseOrderStatus? OrderStatus,
    DateOnly? FromDate,
    DateOnly? ToDate) : IRequest<Result<List<SupplierPurchaseOrderDto>>>;
    
}
