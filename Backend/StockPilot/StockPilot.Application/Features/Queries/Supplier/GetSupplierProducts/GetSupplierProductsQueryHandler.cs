using MediatR;
using Microsoft.EntityFrameworkCore;
using StockPilot.Application.Common.Extensions;
using StockPilot.Application.Common.Interfaces.Data;
using StockPilot.Application.Common.Model;
using StockPilot.Application.Dtos;
using StockPilot.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockPilot.Application.Features.Queries.Supplier.GetSupplierProducts
{
    public sealed record GetSupplierProductsQueryHandler(IAppDbContext context) : IRequestHandler<GetSupplierProductsQuery, Result<PaginatedList<ProductDto>>>
    {
        public async Task<Result<PaginatedList<ProductDto>>> Handle(GetSupplierProductsQuery request, CancellationToken cancellationToken)
        {
            var query = context.purchaseOrders
                .AsNoTracking()
                .Where(s => s.SupplierId == request.SupplierId)
                .SelectMany(s => s.Items)
                .Include(s => s.Product)
                .Select(s => new ProductDto
                {
                    Name = s.Product.Name,
                    Description = s.Product.Description,
                    Sku = s.Product.Sku,
                    CategoryId = s.Product.CategoryId,
                    Unit = s.Product.Unit,
                    CostPrice = s.Product.CostPrice,
                    SellingPrice = s.Product.SellingPrice,
                    CurrentStock = s.Product.CurrentStock,
                    MinimumStock = s.Product.MinimumStock,
                    ReorderQuantity = s.Product.ReorderQuantity,
                    ImageUrl = s.Product.ImageUrl,
                });

            var paginated = await QueryableExtensions.PaginatedAsync(query, request.pageSize, request.PageNumber);
              
            return Result<PaginatedList<ProductDto>>.Success(paginated);
        }
    }
}
