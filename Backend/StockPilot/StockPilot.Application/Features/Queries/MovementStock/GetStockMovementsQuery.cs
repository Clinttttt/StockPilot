using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using static StockPilot.Domain.Entities.StockMovement;

namespace StockPilot.Application.Features.Queries.MovementStock
{
    public sealed record GetStockMovementsQuery(Guid ProductId, StockMovementType? Type = null) : IRequest<IReadOnlyList<StockMovementsDto>>;
  
}
