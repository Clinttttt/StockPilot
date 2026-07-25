using MediatR;
using Microsoft.EntityFrameworkCore;
using StockPilot.Application.Common.Interfaces.Data;
using StockPilot.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockPilot.Application.Features.Command.PurchaseOrder.SubmitPurchaseOrder
{
    internal class SubmitPurchaseOrderCommandHandler(IAppDbContext context) : IRequestHandler<SubmitPurchaseOrderCommand, Result>
    {
        public async Task<Result> Handle(SubmitPurchaseOrderCommand request, CancellationToken cancellationToken)
        {
            var purchaseOrder = await context.purchaseOrders
                .FirstOrDefaultAsync(s => s.Id == request.PurchaseOrder
                && s.orderStatus == Domain.Entities.PurchaseOrderStatus.Draft,
                cancellationToken);

            if (purchaseOrder is null)
                return Result.NotFound("Purchase order not found");

            purchaseOrder.orderStatus = request.Status ?? purchaseOrder.orderStatus;
            await context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
