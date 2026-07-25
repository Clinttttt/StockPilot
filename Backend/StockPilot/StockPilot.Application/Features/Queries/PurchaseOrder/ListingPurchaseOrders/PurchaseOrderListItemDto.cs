using StockPilot.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockPilot.Application.Features.Queries.PurchaseOrder.ListingPurchaseOrders
{
    public sealed record PurchaseOrderListItemDto(
     Guid PurchaseOrderId,
     string PoNumber,
     string SupplierName,
     DateTime? OrderDate,
     DateTime? ExpectedDeliveryDate,
     PurchaseOrderStatus? Status,
     int ItemCount,
     decimal TotalAmount
 );
}
