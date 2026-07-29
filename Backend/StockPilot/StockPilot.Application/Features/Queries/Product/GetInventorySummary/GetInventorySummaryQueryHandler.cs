using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using StockPilot.Application.Common.Interfaces.Data;
using StockPilot.Application.Dtos;
using StockPilot.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.XPath;

namespace StockPilot.Application.Features.Queries.Product.GetInventorySummary
{
    internal class GetInventorySummaryQueryHandler(IAppDbContext context) : IRequestHandler<GetInventorySummaryQuery, Result<InventorySummaryDto>>
    {
        public async Task<Result<InventorySummaryDto>> Handle(GetInventorySummaryQuery request, CancellationToken cancellationToken)
        {
            var query = context.products
                .AsNoTracking()
                .Where(s=> s.IsActive);

            var totalProducts = await query
                .CountAsync(cancellationToken);

            var activeProducts = await query
                .CountAsync(
                    product => product.IsActive,
                    cancellationToken);

            var totalUnitsInStock = await query
                .SumAsync(
                    product => product.CurrentStock,
                    cancellationToken);

            var lowStockProducts = await query
                .CountAsync(
                    product => product.CurrentStock * 2 >= product.MinimumStock
                    && product.CurrentStock <= product.MinimumStock
                    && product.CurrentStock > 0,
                    cancellationToken);

            var outOfStockProducts = await query
                .CountAsync(
                    product => product.CurrentStock <= 0,
                    cancellationToken);

            var totalInventoryCost = await query
                .SumAsync(
                    product => product.CurrentStock * product.CostPrice,
                    cancellationToken);

            var potentialSalesValue = await query.SumAsync(
                product => product.CurrentStock * product.SellingPrice,
                cancellationToken
                );

            var inventorySummary = new InventorySummaryDto(
                TotalProducts: totalProducts,
                ActiveProducts: activeProducts,
                TotalUnitsInStock: totalUnitsInStock,
                LowStockProducts: lowStockProducts,
                OutOfStockProducts: outOfStockProducts,
                TotalInventoryCost: totalInventoryCost,
                PotentialSalesValue: potentialSalesValue
                );
            return Result<InventorySummaryDto>.Success(inventorySummary);

        }
    }
}
