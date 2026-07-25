using StockPilot.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockPilot.Application.Features.Queries.PurchaseOrder.GetPurchaseOrderById
{
    public sealed record PurchaseOrderDto(
    Guid PurchaseOrderId,
    string PoNumber,
    string SupplierName,
    DateTime? OrderDate,
    DateTime? ExpectedDeliveryDate,
    PurchaseOrderStatus? Status,
    int ItemCount,
    decimal TotalAmount,
    string Remarks,
     List<PurchaseOrderItemDto> Items
 );
    public record PurchaseOrderItemDto(
         Guid ProductId,
         string ProductName,
         int QuantityOrdered,
         int QuantityReceived,
         decimal UnitCost
        );
}
