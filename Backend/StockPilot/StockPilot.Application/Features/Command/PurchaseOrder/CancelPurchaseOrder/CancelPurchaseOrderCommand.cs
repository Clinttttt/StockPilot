using MediatR;
using StockPilot.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockPilot.Application.Features.Command.PurchaseOrder.CancelPurchaseOrder
{
    public sealed record CancelPurchaseOrderCommand(Guid PurchaseOrderId, string Reason) : IRequest<Result>;
   
}
