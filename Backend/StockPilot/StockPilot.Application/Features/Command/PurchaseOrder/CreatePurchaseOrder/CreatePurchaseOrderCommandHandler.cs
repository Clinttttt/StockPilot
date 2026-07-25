using MediatR;
using Microsoft.EntityFrameworkCore;
using StockPilot.Application.Common.Interfaces.Data;
using StockPilot.Domain.Common;
using StockPilot.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Reflection.Metadata;
using System.Text;

namespace StockPilot.Application.Features.Command.PurchaseOrder.CreatePurchaseOrder
{
    public class CreatePurchaseOrderCommandHandler(IAppDbContext context) : IRequestHandler<CreatePurchaseOrderCommand, Result>
    {
        public async Task<Result> Handle(CreatePurchaseOrderCommand request, CancellationToken cancellationToken)
        {
            if (!await context.suppliers.AnyAsync(s => s.Id == request.SupplierId && s.IsActive))
                return Result.NotFound("Suppliers not found");
            if(!request.order.Any())
                return Result.Failure("Purchase order must contain at least one item.");

            await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

            try
            {
                var PurchaseOrders = StockPilot.Domain.Entities.PurchaseOrder.Create(request.SupplierId, request.ExpectedDeliveryDate, request.Remarks);
                PurchaseOrders.PoNumber = StockPilot.Domain.Entities.PurchaseOrder.GeneratePoNum(PurchaseOrders.Id);
                context.purchaseOrders.Add(PurchaseOrders);
                await context.SaveChangesAsync(cancellationToken);

                var purchaseOrderItems = request.order.Select(s => new PurchaseOrderItem
                {
                    PurchaseOrderId = PurchaseOrders.Id,
                    ProductId = s.ProductId,
                    QuantityOrdered = s.Quantity,
                    UnitCost = s.Cost
                   
                }).ToList();

                context.purchaseOrderItems.AddRange(purchaseOrderItems);
                await context.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
            }
            catch
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        
            return Result.Success();
        }
    }
}
