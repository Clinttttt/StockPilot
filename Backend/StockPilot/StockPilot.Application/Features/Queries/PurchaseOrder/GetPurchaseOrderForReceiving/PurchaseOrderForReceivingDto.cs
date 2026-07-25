using StockPilot.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockPilot.Application.Features.Queries.PurchaseOrder.GetPurchaseOrderForReceiving
{
    public sealed record PurchaseOrderForReceivingDto(
    Guid PurchaseOrderId,
    string PoNumber,
    string SupplierName,
    DateTime? OrderDate,
    DateTime? ExpectedDeliveryDate,
    PurchaseOrderStatus? Status,
    int ItemCount,
    decimal TotalAmount,
    string Remarks,
     List<PurchaseOrderItemForReceivingDto> Items
 );

}
