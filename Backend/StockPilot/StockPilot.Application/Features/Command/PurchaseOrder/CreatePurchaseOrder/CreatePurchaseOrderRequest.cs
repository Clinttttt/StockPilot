using System;
using System.Collections.Generic;
using System.Text;

namespace StockPilot.Application.Features.Command.PurchaseOrder.CreatePurchaseOrder
{
    public sealed record CreatePurchaseOrderRequest(Guid ProductId, int Quantity, decimal Cost);
    
}
