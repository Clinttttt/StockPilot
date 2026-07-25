using MediatR;
using StockPilot.Application.Features.Queries.PurchaseOrder.GetPurchaseOrderById;
using StockPilot.Domain.Common;
using StockPilot.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockPilot.Application.Features.Queries.PurchaseOrder.GetPurchaseOrderForReceiving
{
    public sealed record GetPurchaseOrderForReceivingQuery(Guid PurchaseOrderId) : IRequest<Result<PurchaseOrderForReceivingDto>>;
 
}
