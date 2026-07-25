using MediatR;
using StockPilot.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockPilot.Application.Features.Command.PurchaseOrder.CreatePurchaseOrder
{
    public record CreatePurchaseOrderCommand(
        Guid SupplierId, 
        List<CreatePurchaseOrderRequest>
        order,DateTime? ExpectedDeliveryDate,
        string Remarks
        ) : IRequest<Result>;
   
    
}
