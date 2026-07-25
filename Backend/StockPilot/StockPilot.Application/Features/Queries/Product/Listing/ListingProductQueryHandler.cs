using MediatR;
using Microsoft.EntityFrameworkCore;
using StockPilot.Application.Common.Extensions;
using StockPilot.Application.Common.Interfaces.Data;
using StockPilot.Application.Common.Model;
using StockPilot.Domain.Common;
using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Text;

namespace StockPilot.Application.Features.Queries.Product.ListingProduct
{
    public class ListingProductQueryHandler(IAppDbContext context) : IRequestHandler<ListingProductQuery, Result<PaginatedList<ListingProductQueryDto>>>
    {
       public async Task<Result<PaginatedList<ListingProductQueryDto>>> Handle(ListingProductQuery request, CancellationToken cancellationToken)
        {

            var query = context.products.AsNoTracking()
                .Where(s=> s.IsActive == request.IsActive);

            if (request.CategoryId.HasValue)
            {
                query = query.Where(s=> s.CategoryId == request.CategoryId);
            }
            if(request.lowStock == true)
            {
                query = query.Where(s => s.CurrentStock <= s.MinimumStock);
            }
            if (!string.IsNullOrWhiteSpace(request.search))
            {
                query = query.Where(s=> s.Name!.Contains(request.search));
            }

            var data = query.OrderByDescending(s => s.Name)
                .Select(s => new ListingProductQueryDto
              {
                  ImageUrl = s.ImageUrl,
                  ProductId = s.Id,
                  ProductName = s.Name,
                  CurrentStock = s.CurrentStock,
                  SellingPrice = s.SellingPrice,
                  CategoryId = s.CategoryId,
                  IsActive = s.IsActive,
              });

            var paginated = await QueryableExtensions.PaginatedAsync(data, request.PageSize, request.pageNumber);
       
            return Result<PaginatedList<ListingProductQueryDto>>.Success(paginated);
        }
   
    }

}
