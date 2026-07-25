using System;
using System.Collections.Generic;
using System.Text;

namespace StockPilot.Application.Features.Queries.PurchaseOrder.GetPurchaseOrderForReceiving
{
    public sealed record PurchaseOrderItemForReceivingDto(
      Guid PurchaseOrderItemId,
      Guid ProductId,
      string ProductName,
      int QuantityOrdered,
      int QuantityReceived,
      int RemainingQuantity
  );
}
