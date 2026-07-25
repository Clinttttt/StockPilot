using MediatR;
using Microsoft.EntityFrameworkCore;
using StockPilot.Application.Common.Interfaces.Data;
using StockPilot.Domain.Common;
using StockPilot.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace StockPilot.Application.Features.Command.PurchaseOrder.UpdatePurchaseOrder
{
    public class UpdatePurchaseOrderCommandHandler(IAppDbContext context) : IRequestHandler<UpdatePurchaseOrderCommand, Result>
    {
        public async Task<Result> Handle(UpdatePurchaseOrderCommand request, CancellationToken cancellationToken)
        {
            if (!await context.suppliers.AnyAsync(s => s.Id == request.SupplierId && s.IsActive))
                return Result.NotFound("Supplier not found");

            if (!request.Items.Any())
                return Result.Failure("Purchase order must contain at least one item.");

            var purchaseOrders = await context.purchaseOrders
                .Include(s => s.Items)
                .Where(s => s.Id == request.PurchaseOrderId && s.orderStatus == Domain.Entities.PurchaseOrderStatus.Draft)
                .FirstOrDefaultAsync();

            if (purchaseOrders is null)
                return Result.NotFound("Purchase order not found");


            purchaseOrders.SupplierId = request.SupplierId ?? purchaseOrders.SupplierId;
            purchaseOrders.Remarks = request.Remarks ?? purchaseOrders.Remarks;
            purchaseOrders.ExpectedDeliveryDate = request.ExpectedDeliveryDate ?? purchaseOrders.ExpectedDeliveryDate;

            var updated = request.Items.Select(s => StockPilot.Domain.Entities.PurchaseOrderItem.Update(
                QuantityOrdered: s.Quantity,
                UnitCost: s.Cost,
                ProductId: s.ProductId,
                PurchaseOrderId: request.PurchaseOrderId
                ));

            purchaseOrders.Update(updated);
            await context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
