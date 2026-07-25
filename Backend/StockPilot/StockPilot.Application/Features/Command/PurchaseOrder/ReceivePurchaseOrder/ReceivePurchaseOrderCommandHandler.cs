using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StockPilot.Application.Common.Interfaces.Data;
using StockPilot.Domain.Common;
using StockPilot.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockPilot.Application.Features.Command.PurchaseOrder.ReceivePurchaseOrder
{
    public class ReceivePurchaseOrderCommandHandler(IAppDbContext context) : IRequestHandler<ReceivePurchaseOrderCommand, Result>
    {
        public async Task<Result> Handle(ReceivePurchaseOrderCommand request, CancellationToken cancellationToken)
        {
            var purchaseOrders = await context.purchaseOrders
                .Include(s => s.Items)
                .FirstOrDefaultAsync(s => s.Id == request.PurchaseOrderId);

            if (purchaseOrders is null)
                return Result.NotFound("Purchase order not found");

            purchaseOrders.Remarks = request.Remarks;
            purchaseOrders.ReceivedDate = DateTime.UtcNow;


            var itemsDict = purchaseOrders.Items.ToDictionary(s => s.Id);
            foreach (var itemReq in request.Items)
            {

                if (itemsDict.TryGetValue(itemReq.PurchaseOrderIdItem, out var purchaseOrder))
                {
                    purchaseOrder.QuantityReceived = itemReq.QuantityRecieved;
                }
            }

            await context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
