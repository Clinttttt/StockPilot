using MediatR;
using Microsoft.EntityFrameworkCore;
using StockPilot.Application.Common.Interfaces.Data;
using StockPilot.Application.Dtos;
using StockPilot.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockPilot.Application.Features.Queries.Product.GetProduct
{
    internal class GetProductQueryHandler(IAppDbContext context) : IRequestHandler<GetProductQuery, Result<ProductDto>>
    {
        public async Task<Result<ProductDto>> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            var product = await context.products.FirstOrDefaultAsync(s => s.Id == request.ProductId);
            if (product is null) return Result<ProductDto>.NotFound("Product not found");

            return Result<ProductDto>.Success(new ProductDto
            {
                Name = product.Name,
                Description = product.Description,
                Sku = product.Sku,
                CategoryId = product.CategoryId,
                Unit = product.Unit,
                CostPrice = product.CostPrice,
                SellingPrice = product.SellingPrice,
                CurrentStock = product.CurrentStock,
                MinimumStock = product.MinimumStock,
                ReorderQuantity = product.ReorderQuantity,
                ImageUrl = product.ImageUrl,
            });
        }
    }
}
