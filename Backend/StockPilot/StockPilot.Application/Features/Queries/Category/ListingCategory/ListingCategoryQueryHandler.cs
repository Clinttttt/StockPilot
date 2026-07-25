using MediatR;
using Microsoft.EntityFrameworkCore;
using StockPilot.Application.Common.Interfaces.Data;
using StockPilot.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockPilot.Application.Features.Queries.Category.ListingCategory
{
    internal class ListingCategoryQueryHandler(IAppDbContext context) : IRequestHandler<ListingCategoryQuery, Result<List<ListingCategoryQueryDto>>>
    {
        public async Task<Result<List<ListingCategoryQueryDto>>> Handle(ListingCategoryQuery request, CancellationToken cancellationToken)
        {
            var category = await context.Categories
                .AsNoTracking()
                .OrderByDescending(s=> s.CreatedAt)
                .Where(s=> s.IsActive == true)
                .Select(s=> new ListingCategoryQueryDto
                {
                    CategoryId = s.Id,
                    CategoryName = s.Name,
                    Description = s.Description,
                    ProductCount = s.Products == null ? 0 : s.Products.Count,
                    IsActive = s.IsActive

                }).ToListAsync();
            
            return Result<List<ListingCategoryQueryDto>>.Success(category);
        }
    }
}
