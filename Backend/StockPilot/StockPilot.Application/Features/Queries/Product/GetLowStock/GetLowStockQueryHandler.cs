using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StockPilot.Application.Common.Extensions;
using StockPilot.Application.Common.Interfaces.Data;
using StockPilot.Application.Common.Model;
using StockPilot.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Net.WebRequestMethods;
namespace StockPilot.Application.Features.Queries.Product.GetLowStock
{
    internal class GetLowStockQueryHandler(IAppDbContext context) : IRequestHandler<GetLowStockQuery, Result<PaginatedList<LowStockProductDto>>>
    {
        public async Task<Result<PaginatedList<LowStockProductDto>>> Handle(GetLowStockQuery request, CancellationToken cancellationToken)
        {
            var query = context.products.AsNoTracking()
                .Where(s => s.CurrentStock <= s.MinimumStock);

            var search = request.Search?.Trim() ?? string.Empty;

            query = query
                .WhereIf(
                  !string.IsNullOrWhiteSpace(search),
                  filter => filter.Sku.Contains(search) ||
                  filter.Name.Contains(search))
                .WhereIf(
                  request.CategoryId is not null,
                  filter => filter.CategoryId == request.CategoryId);

            var dto = query.Select(s => new LowStockProductDto(
                ProductId: s.Id,
                ProductName: s.Name,
                Sku: s.Sku,
                CategoryName: s.Category.Name,
                Unit: s.Unit,
                CurrentStock: s.CurrentStock,
                MinimumStock: s.MinimumStock,
                ReorderQuantity: s.ReorderQuantity,
                ShortageQuantity: s.MinimumStock - s.CurrentStock,
                Level: s.CurrentStock <= 0 ?
                        LowStockLevel.OutOfStock
                        : s.CurrentStock * 2 <= s.MinimumStock
                        ? LowStockLevel.Critical
                        : LowStockLevel.LowStock
                )
           );

            var paginated = await QueryableExtensions.PaginatedAsync(dto, request.PageSize, request.PageNumber);
            return Result<PaginatedList<LowStockProductDto>>.Success(paginated);
        }
    }
}

