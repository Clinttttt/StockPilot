using MediatR;
using StockPilot.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;
using static StockPilot.Domain.Entities.Enums;
using static StockPilot.Domain.Entities.StockMovement;

namespace StockPilot.Application.Features.Command.Product.AdjustStock
{
    public sealed record AdjustStockCommand(
        Guid ProductId,
        StockAdjustmentType Type,
        StockMovementType MovementType,
        string? Remarks,
        string? Reason,
        int Quantity,
        Guid? PurchaseOrderId = null
        ) : IRequest<Result>;
    
}
