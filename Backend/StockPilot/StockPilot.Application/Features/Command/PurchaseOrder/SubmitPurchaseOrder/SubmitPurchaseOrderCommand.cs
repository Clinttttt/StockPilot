using MediatR;
using StockPilot.Domain.Common;
using StockPilot.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockPilot.Application.Features.Command.PurchaseOrder.SubmitPurchaseOrder
{
    public sealed record SubmitPurchaseOrderCommand(Guid PurchaseOrder, PurchaseOrderStatus? Status) : IRequest<Result>;

}
