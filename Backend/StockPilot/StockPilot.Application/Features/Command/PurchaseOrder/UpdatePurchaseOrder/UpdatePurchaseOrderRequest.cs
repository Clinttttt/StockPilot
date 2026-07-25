using System;
using System.Collections.Generic;
using System.Text;

namespace StockPilot.Application.Features.Command.PurchaseOrder.UpdatePurchaseOrder
{
    public record UpdatePurchaseOrderRequest(Guid ProductId, int Quantity, decimal Cost);
}
