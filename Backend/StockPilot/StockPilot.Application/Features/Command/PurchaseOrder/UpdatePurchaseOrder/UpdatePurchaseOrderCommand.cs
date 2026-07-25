using MediatR;
using StockPilot.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockPilot.Application.Features.Command.PurchaseOrder.UpdatePurchaseOrder
{
    public sealed record UpdatePurchaseOrderCommand(
        Guid PurchaseOrderId,
        Guid? SupplierId,
        List<UpdatePurchaseOrderRequest> Items,
        DateTime? ExpectedDeliveryDate,
        string Remarks) : IRequest<Result>;
 
}
