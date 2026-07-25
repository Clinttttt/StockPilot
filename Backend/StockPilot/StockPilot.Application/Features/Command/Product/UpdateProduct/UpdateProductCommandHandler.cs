using MediatR;
using Microsoft.EntityFrameworkCore;
using StockPilot.Application.Common.Interfaces.Data;
using StockPilot.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockPilot.Application.Features.Command.Product.UpdateProduct
{
    internal class UpdateProductCommandHandler(IAppDbContext context) : IRequestHandler<UpdateProductCommand, Result>
    {
        public async Task<Result> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {

            var product = await context.products.FirstOrDefaultAsync(s => s.Id == request.Id);
            if (product is null) return Result<bool>.NotFound("Product not found");


            product.Update(request.ProductName, request.SKU, request.currentStock, request.minimumStock, request.costPrice,request.Unit);
            await context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
