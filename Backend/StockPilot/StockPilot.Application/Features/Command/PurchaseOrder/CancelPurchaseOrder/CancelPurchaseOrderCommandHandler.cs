using MediatR;
using Microsoft.EntityFrameworkCore;
using StockPilot.Application.Common.Interfaces.Data;
using StockPilot.Domain.Common;
using StockPilot.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Runtime.InteropServices;
using System.Text;

namespace StockPilot.Application.Features.Command.PurchaseOrder.CancelPurchaseOrder
{
    internal class CancelPurchaseOrderCommandHandler(IAppDbContext context) : IRequestHandler<CancelPurchaseOrderCommand, Result>
    {
        public async Task<Result> Handle(CancelPurchaseOrderCommand request, CancellationToken cancellationToken)
        {
            var purchaseOrder = await context.purchaseOrders
                .FirstOrDefaultAsync(s => s.Id == request.PurchaseOrderId,cancellationToken);

            if (purchaseOrder is null)
                return Result.NotFound("Purchase order not found");

            purchaseOrder.orderStatus = PurchaseOrderStatus.Cancelled;
            purchaseOrder.Remarks = request.Reason ?? purchaseOrder.Remarks;

           await context.SaveChangesAsync(cancellationToken);

            return Result.Success();

        }
    }
}
