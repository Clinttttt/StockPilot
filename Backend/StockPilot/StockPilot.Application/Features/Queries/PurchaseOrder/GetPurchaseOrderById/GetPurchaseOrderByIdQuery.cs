using MediatR;
using StockPilot.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockPilot.Application.Features.Queries.PurchaseOrder.GetPurchaseOrderById
{
    public sealed record GetPurchaseOrderByIdQuery(Guid PurchaseOrderId) : IRequest<Result<PurchaseOrderDto>>;
    
}
