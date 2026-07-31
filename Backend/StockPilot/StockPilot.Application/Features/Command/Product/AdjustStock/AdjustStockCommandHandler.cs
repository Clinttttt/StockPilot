using MediatR;
using Microsoft.EntityFrameworkCore;
using StockPilot.Application.Common.Interfaces.Data;
using StockPilot.Domain.Common;
using StockPilot.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using static StockPilot.Domain.Entities.Enums;
using static StockPilot.Domain.Entities.StockMovement;
namespace StockPilot.Application.Features.Command.Product.AdjustStock
{
    internal class AdjustStockCommandHandler(IAppDbContext context) : IRequestHandler<AdjustStockCommand, Result>
    {
        public async Task<Result> Handle(AdjustStockCommand request, CancellationToken cancellationToken)
        {
            var product = await context.products
                .FirstOrDefaultAsync(s => s.Id == request.ProductId, cancellationToken);

            if (product is null)
                return Result.NotFound("Product not found");

            if (request.Quantity <= 0)
                return Result.Failure("Quantity must be greater than zero.");

            if (request.Type == StockAdjustmentType.Increase)
            {
                product.CurrentStock += request.Quantity;
            }

            else if (request.Type == StockAdjustmentType.Decrease)
            {
                if (product.CurrentStock < request.Quantity)
                    return Result.Failure("Request quantity cannot be greater than current stock");

                product.CurrentStock -= request.Quantity;
            }
            else
            {
                return Result.Failure("Invalid stock adjustment type.");
            }

            var stockMovement = StockMovement.Create(request.ProductId,
                   request.MovementType,
                   request.Quantity,
                   request.Reason,
                   request.Remarks
                   );

            context.stocksMovements.Add(stockMovement);
            await context.SaveChangesAsync(cancellationToken);
            return Result.Success();

        }
    }
}
/*public sealed record LowStockProductDto(
 Guid ProductId,
 string ProductName,
 string Sku,
 string CategoryName,
 string Unit,
 int CurrentStock,
 int MinimumStock,
 int ReorderQuantity,
 int ShortageQuantity,
 LowStockLevel Level
);
public enum LowStockLevel
{
    OutOfStock = 1,
    Critical = 2,
    LowStock = 3
}*/

