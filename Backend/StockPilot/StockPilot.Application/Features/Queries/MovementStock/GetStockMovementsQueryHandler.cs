using MediatR;
using Microsoft.EntityFrameworkCore;
using StockPilot.Application.Common.Interfaces.Data;
using StockPilot.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockPilot.Application.Features.Queries.MovementStock
{
    public class GetStockMovementsQueryHandler(IAppDbContext context) : IRequestHandler<GetStockMovementsQuery, Result<IReadOnlyList<StockMovementsDto>>>
    {
        public async Task<Result<IReadOnlyList<StockMovementsDto>>> Handle(GetStockMovementsQuery request, CancellationToken cancellationToken)
        {
            var stockMovements = context.stocks
                .AsNoTracking()
                .Where(s => s.ProductId == request.ProductId);

            if(request.Type != null)
            {
                stockMovements = stockMovements.Where(s => s.Type == request.Type);
            }

            var dto = await stockMovements.Select(s => new StockMovementsDto(

                ProductId: s.ProductId,
                Type: s.Type,
                Quantity: s.Quantity,
                ReferenceNo: s.ReferenceNo ?? string.Empty,
                Reason: s.Reason ?? string.Empty,
                Remarks: s.Remarks ?? string.Empty,
                PurchaseOrderId: s.PurchaseOrderId ?? Guid.Empty
                )).ToListAsync(cancellationToken);

            return Result<IReadOnlyList<StockMovementsDto>>.Success(dto);
        }
    }
}
