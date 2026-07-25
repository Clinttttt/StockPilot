using MediatR;
using StockPilot.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockPilot.Application.Features.Command.PurchaseOrder.ReceivePurchaseOrder
{
    public sealed record ReceivePurchaseOrderCommand(
        Guid PurchaseOrderId,
        IReadOnlyList<ReceivePurchaseOrderItem> Items,
        DateTime ReceievedDate,
        string Remarks) : IRequest<Result>;

}
