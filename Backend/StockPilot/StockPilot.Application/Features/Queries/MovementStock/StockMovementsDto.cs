using System;
using System.Collections.Generic;
using System.Text;
using static StockPilot.Domain.Entities.StockMovement;

namespace StockPilot.Application.Features.Queries.MovementStock
{
    public sealed record class StockMovementsDto(
        Guid ProductId,
        StockMovementType Type,
        int Quantity,
        string ReferenceNo,
        string Reason,
        string Remarks,
        Guid? PurchaseOrderId = null
        );
}
